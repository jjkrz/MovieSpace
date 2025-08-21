using Application.Abstractions;

namespace Application.MoviePeople.UpdateMoviePerson
{
    public record UpdateMoviePersonCommand(Guid Id, string FirstName, string LastName) : ICommand
    {
    }
}

