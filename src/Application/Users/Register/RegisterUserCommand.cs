using Application.Abstractions;

namespace Application.Users.Register
{
    public sealed record RegisterUserCommand(string UserName, string Email, string Password): ICommand<RegisterResponse>
    {
    }
}
