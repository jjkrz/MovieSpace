using Domain.Common;

namespace Infrastructure.Persistance.Services.AuthService
{
    public static class AuthErrors
    {
        public static Error RegistrationFailed(string messages) => new Error(ErrorType.Validation, messages);
        public static Error UserNotFound => new Error(ErrorType.NotFound, "User was not found");
        public static Error InvalidCredentials => new Error(ErrorType.Unauthorized, "Invalid credentials");
        public static Error TokenGenerationFailed(string message) => new Error(ErrorType.Unexpected, message);
    }
}
