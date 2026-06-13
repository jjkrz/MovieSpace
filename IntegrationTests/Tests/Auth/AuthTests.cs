using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.Users.Login;
using Application.Users.Register;
using FluentAssertions;
using MovieSpace.IntegrationTests.Fixtures;

namespace MovieSpace.IntegrationTests.Tests.Auth;

// ApiFactory is expensive to start (Docker + migrations), so we share one instance
// across all test methods in this class via IClassFixture.
// IAsyncLifetime gives us a per-test InitializeAsync that wipes the DB via Respawn.
public class AuthTests : IClassFixture<ApiFactory>, IAsyncLifetime
{
    private readonly ApiFactory _factory;
    private readonly HttpClient _client;

    public AuthTests(ApiFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync() => await _factory.ResetDatabaseAsync();

    public Task DisposeAsync() => Task.CompletedTask;


    [Fact]
    public async Task Register_ValidData_Returns200WithUserId()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            userName = "john",
            email = "john@example.com",
            password = "Password1!"
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<RegisterResponse>();
        body!.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Register_DuplicateEmail_Returns200_BecauseIdentityDoesNotEnforceEmailUniqueness()
    {
        // ASP.NET Identity enforces unique *UserName*, not unique email by default.
        // Two different accounts with the same email are allowed.
        await _client.PostAsJsonAsync("/api/auth/register", new
        {
            userName = "alice",
            email = "shared@example.com",
            password = "Password1!"
        });

        var response = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            userName = "alice2",
            email = "shared@example.com",
            password = "Password1!"
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Register_DuplicateUserName_Returns400()
    {
        await _client.PostAsJsonAsync("/api/auth/register", new
        {
            userName = "bob",
            email = "bob@example.com",
            password = "Password1!"
        });

        var response = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            userName = "bob",
            email = "bob2@example.com",
            password = "Password1!"
        });

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_WeakPassword_Returns400()
    {
        // Identity requires: min 8 chars + at least one digit
        var response = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            userName = "charlie",
            email = "charlie@example.com",
            password = "weak"
        });

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_EmptyFields_Returns400()
    {
        // FluentValidation rejects empty strings before hitting Identity
        var response = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            userName = "",
            email = "",
            password = ""
        });

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Login_ValidCredentials_Returns200WithBearerToken()
    {
        await _client.PostAsJsonAsync("/api/auth/register", new
        {
            userName = "dave",
            email = "dave@example.com",
            password = "Password1!"
        });

        var response = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            email = "dave@example.com",
            password = "Password1!"
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<LoginResponse>();
        body!.Token.Should().NotBeNullOrWhiteSpace();
        body.Expiration.Should().BeAfter(DateTime.UtcNow);
    }

    [Fact]
    public async Task Login_WrongPassword_Returns401()
    {
        await _client.PostAsJsonAsync("/api/auth/register", new
        {
            userName = "eve",
            email = "eve@example.com",
            password = "Password1!"
        });

        var response = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            email = "eve@example.com",
            password = "WrongPassword1!"
        });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_NonExistentUser_Returns404()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            email = "ghost@example.com",
            password = "Password1!"
        });

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Login_EmptyFields_Returns400()
    {
        // FluentValidation rejects empty email/password before hitting AuthService
        var response = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            email = "",
            password = ""
        });

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task ProtectedEndpoint_NoToken_Returns401()
    {
        var anyMovieId = Guid.NewGuid();

        var response = await _client.PostAsJsonAsync(
            $"/api/movies/{anyMovieId}/rate",
            new { score = 8 });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ProtectedEndpoint_InvalidToken_Returns401()
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "this.is.not.a.valid.jwt");

        var anyMovieId = Guid.NewGuid();
        var response = await _client.PostAsJsonAsync(
            $"/api/movies/{anyMovieId}/rate",
            new { score = 8 });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task FullAuthFlow_RegisterThenLoginThenAccessProtectedEndpoint_TokenIsAccepted()
    {
        // 1. Register
        await _client.PostAsJsonAsync("/api/auth/register", new
        {
            userName = "frank",
            email = "frank@example.com",
            password = "Password1!"
        });

        // 2. Login — get JWT token
        var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            email = "frank@example.com",
            password = "Password1!"
        });
        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var loginBody = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        loginBody!.Token.Should().NotBeNullOrWhiteSpace();

        // 3. Attach the token and hit a protected endpoint
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", loginBody.Token);

        // Non-existent movieId → business error (404), not auth error (401/403).
        // This proves the JWT was accepted by the middleware.
        var nonExistentMovieId = Guid.NewGuid();
        var rateResponse = await _client.PostAsJsonAsync(
            $"/api/movies/{nonExistentMovieId}/rate",
            new { score = 8 });

        rateResponse.StatusCode.Should().NotBe(HttpStatusCode.Unauthorized);
        rateResponse.StatusCode.Should().NotBe(HttpStatusCode.Forbidden);
    }
}
