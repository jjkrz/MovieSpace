using System.Security.Claims;
using Application.Abstractions;
using Domain.Common;
using Infrastructure.Persistance.Services.Jwt.Errors;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Persistance.Services.Jwt
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor; 
        }
        public Result<Guid> GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                             ?? _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value
                             ?? _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Result.Failure<Guid>(JwtErrors.IdFetchingFailed);

            return Result.Success<Guid>(Guid.Parse(userIdClaim));
        }
    }
}
