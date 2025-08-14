using Domain.Common;

namespace Infrastructure.Persistance.Services.Jwt.Errors
{
    public static class JwtErrors
    {
        public static Error TokenGenerationFailed(string message) => new Error(ErrorType.Unexpected, message);
        public static Error IdFetchingFailed => new Error(ErrorType.NullValue, "Could not fetch UserId");

    }
}
