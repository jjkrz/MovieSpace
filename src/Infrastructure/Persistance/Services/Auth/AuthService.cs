using Application.Abstractions;
using Application.Users.Login;
using Application.Users.Register;
using Domain.Common;
using Infrastructure.Persistance.Identity;
using Infrastructure.Persistance.Services.Jwt;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistance.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UserManager<ApplicationUser> userManager, JwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<RegisterResponse>> RegisterAsync(string userName, string email, string password)
        {
            var user = new ApplicationUser(userName, email);

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return Result.Failure<RegisterResponse>(AuthErrors.RegistrationFailed(string.Join(", ", errorMessages)));
            }

            return new RegisterResponse
            {
                Id = user.Id,
            };
        }

        public async Task<Result<LoginResponse>> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Result.Failure<LoginResponse>(AuthErrors.UserNotFound);

            var result = await _userManager.CheckPasswordAsync(user, password);

            if (!result)
                return Result.Failure<LoginResponse>(AuthErrors.InvalidCredentials);


            var token = _jwtTokenGenerator.GenerateToken(user);


            if (token.IsFailure)
                return Result.Failure<LoginResponse>(token.Error);

            return Result.Success(new LoginResponse
            {
                Token = token.Value.Token,
                Expiration = token.Value.Expiration
            });
        }
    }
}
