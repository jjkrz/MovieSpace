using Domain.Sessions;

namespace Application.Abstractions
{
    public interface ISessionRepository
    {
        Task<Session?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task CreateAsync(Session session, CancellationToken cancellationToken = default);
    }
}
