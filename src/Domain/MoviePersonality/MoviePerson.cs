using Domain.Common;
using Domain.MoviePersonality;
using Domain.Movies;

namespace Domain.Actors
{
    public sealed class MoviePerson : Entity
    {
        public MoviePerson(Guid Id) : base(Id)
        {
        }

        private readonly List<Movie> Movies = [];
        private readonly List<MoviePersonRole> Roles = [];
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
    }
}
