using Application.Users.Login;
using Application.Users.Register;
using Domain.Common;

namespace Application.Abstractions
{
    public interface IAuthService
    {
        Task<Result<RegisterResponse>> RegisterAsync(string userName, string email, string password);
        Task<Result<LoginResponse>> LoginAsync(string email, string password);
    }
}
