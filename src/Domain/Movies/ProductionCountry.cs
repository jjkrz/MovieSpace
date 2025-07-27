using Domain.Common;

namespace Domain.Movies
{
    public sealed class ProductionCountry : Entity
    {
        public ProductionCountry(Guid Id) : base(Id)
        {
        }

        private readonly List<Movie> Movies = [];
        public string Name { get; private set; } = null!;
    }
}
