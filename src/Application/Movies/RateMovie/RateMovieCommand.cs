using Application.Abstractions;

namespace Application.Movies.RateMovie
{
    public record RateMovieCommand(Guid MovieId, int Score): ICommand
    {
    }
}
