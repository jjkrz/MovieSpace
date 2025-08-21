using Domain.MoviePersonality;

namespace Application.Abstractions
{
    public interface IMovieRoleRepository
    {
        Task<bool> ExistsWithName(string RoleName, CancellationToken cancellationToken = default);
        Task CreateAsync(MovieRole movieRole, CancellationToken cancellationToken = default);
        Task<MovieRole?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Delete(MovieRole role);
        Task<ICollection<MovieRole>> GetPaginatedAsync(int page, int size, string? search = null, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
    }
}
