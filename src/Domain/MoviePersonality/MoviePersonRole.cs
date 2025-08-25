using Domain.MoviePeople;
using Domain.Movies;

namespace Domain.MoviePersonality
{
    public sealed class MoviePersonRole
    {
        private MoviePersonRole()
        {
            
        }

        public MoviePersonRole(Guid movieId, Guid moviePersonId, Guid movieRoleId, string? characterName)
        {
            MovieId = movieId;
            MoviePersonId = moviePersonId;
            MovieRoleId = movieRoleId;
            CharacterName = characterName;
        }

        public Guid MovieId { get; private set; }
        public Movie Movie { get; private set; } = null!;

        public Guid MoviePersonId { get; private set; }
        public MoviePerson MoviePerson { get; private set; } = null!;

        public string? CharacterName { get; private set; }

        public Guid MovieRoleId { get; private set; }
        public MovieRole MovieRole { get; private set; } = null!;
    }
}
