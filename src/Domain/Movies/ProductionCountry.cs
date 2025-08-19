using Domain.Common;

namespace Domain.Movies
{
    public sealed class ProductionCountry: Entity
    {
        private ProductionCountry() { }

        private ProductionCountry(string name)
        {
            Name = name;
        }

        private readonly List<Movie> Movies = [];
        public string Name { get; private set; } = null!;

        public static Result<ProductionCountry> CreateProductionCountry(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return Result.Failure<ProductionCountry>(Error.NullValue);

            var country = new ProductionCountry(countryName);

            return Result.Success(country);
        }
    }
}
