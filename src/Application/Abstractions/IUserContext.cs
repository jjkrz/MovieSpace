using Domain.Common;

namespace Application.Abstractions
{
    public interface IUserContext
    {
        Result<Guid> GetCurrentUserId();
    }
}
