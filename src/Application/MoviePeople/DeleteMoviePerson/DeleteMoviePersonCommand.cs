using Application.Abstractions;

namespace Application.MoviePeople.DeleteMoviePerson
{
    public record DeleteMoviePersonCommand(Guid Id) : ICommand
    {
    }
}

