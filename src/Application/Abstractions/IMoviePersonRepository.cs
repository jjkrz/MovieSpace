using Domain.MoviePeople;

namespace Application.Abstractions
{
    public interface IMoviePersonRepository
    {
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<ICollection<MoviePerson>> GetPaginatedAsync(int page, int size, string? search = null, CancellationToken cancellationToken = default);
        Task CreateAsync(MoviePerson person, CancellationToken cancellationToken = default);
        Task<MoviePerson?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<MoviePerson>> GetByIdsAsync(ICollection<Guid> ids, CancellationToken cancellationToken = default); 
        void Delete(MoviePerson person);
    }
}

