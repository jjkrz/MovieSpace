using Application.Abstractions;

namespace Application.Movies.AddGenreToMovie
{
    public record AddGenreToMovieCommand(Guid movieId, Guid genreId) : ICommand
    {
    }
}
