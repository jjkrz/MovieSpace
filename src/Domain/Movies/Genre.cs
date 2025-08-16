using Domain.Common;

namespace Domain.Movies
{
    public sealed class Genre: Entity
    {
        private List<Movie> Movies = [];
        public string Name { get; private set; } = null!;
    }
}
