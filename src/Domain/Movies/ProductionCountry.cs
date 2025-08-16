using Domain.Common;

namespace Domain.Movies
{
    public sealed class ProductionCountry: Entity
    {
        private readonly List<Movie> Movies = [];
        public string Name { get; private set; } = null!;
    }
}
