using Application.Abstractions;

namespace Application.MoviePeople.AddMoviePerson
{
    public record AddMoviePersonCommand(string FirstName, string LastName) : ICommand<AddMoviePersonResponse>
    {
    }
}

