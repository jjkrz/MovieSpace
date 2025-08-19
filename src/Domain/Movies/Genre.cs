using Domain.Common;

namespace Domain.Movies
{
    public sealed class Genre: Entity
    {
        private Genre() { }

        private Genre(string name)
        {
            Name = name;
        }

        private List<Movie> Movies = [];
        public string Name { get; private set; } = null!;

        public static Result<Genre> CreateGenre(string genreName)
        {
            if (string.IsNullOrWhiteSpace(genreName))
                return Result.Failure<Genre>(Error.NullValue);

            var genre = new Genre(genreName);

            return Result.Success(genre);
        }
    }
}
