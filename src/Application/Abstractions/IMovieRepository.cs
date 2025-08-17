using Domain.Movies;

namespace Application.Abstractions
{
    public interface IMovieRepository
    {
        Task<long> GetMoviesCountAsync(
                string? search = null,
                CancellationToken cancellationToken = default);
        Task<List<Movie>> GetMoviesAsync(
                    int page,
                    int size,
                    string? search = null,
                    CancellationToken cancellationToken = default);
        Task CreateAsync(Movie movie, CancellationToken cancellationToken);
        Task<Movie?> GetByIdWithRatingAsync(Guid Id, CancellationToken cancellationToken);

        Task<Movie?> GetByIdWithGenres(
                    Guid Id,
                    CancellationToken cancellationToken);
    }
}
