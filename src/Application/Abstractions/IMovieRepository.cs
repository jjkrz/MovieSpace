using Domain.Movies;

namespace Application.Abstractions
{
    public interface IMovieRepository
    {
        Task CreateAsync(Movie movie, CancellationToken cancellationToken);
        Task<Movie?> GetByIdWithRatingAsync(Guid Id, CancellationToken cancellationToken);
    }
}
