using Domain.Common;
using Domain.MoviePersonality;
using Domain.Movies;

namespace Domain.MoviePeople
{
    public sealed class MoviePerson : Entity
    {
        private readonly List<Movie> Movies = [];
        private readonly List<MoviePersonRole> Roles = [];
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;

        private MoviePerson() { }

        private MoviePerson(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Result<MoviePerson> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<MoviePerson>(Error.NullValue);

            return Result.Success(new MoviePerson(firstName, lastName));
        }

        public Result Update(string? firstName, string? lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
                return Result.Failure(Error.NullValue);
            if (!string.IsNullOrWhiteSpace(firstName))
                FirstName = firstName;
            if (!string.IsNullOrWhiteSpace(lastName))
                LastName = lastName;
            return Result.Success();
        }
    }
}
