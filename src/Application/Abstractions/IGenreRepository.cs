using Domain.Movies;

namespace Application.Abstractions
{
    public interface IGenreRepository
    {
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);

        Task<ICollection<Genre>> GetPaginatedAsync(
            int page,
            int size,
            string? search = null,
            CancellationToken cancellationToken = default);
        Task<bool> ExistsWithName(string name, CancellationToken cancellationToken = default);
        Task CreateAsync(Genre genre, CancellationToken cancellationToken = default);
        Task<Genre?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);
        void Delete(Genre genre);
    }
}
