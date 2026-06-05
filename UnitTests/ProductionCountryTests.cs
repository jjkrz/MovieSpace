using Domain.Movies;

namespace MovieSpace.UnitTests
{
    public class ProductionCountryTests
    {
        [Fact]
        public void CreateProductionCountry_ShouldSucceed_WithValidCountryName()
        {
            var countryName = "United States";

            var result = ProductionCountry.CreateProductionCountry(countryName);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(countryName, result.Value.Name);
        }

        [Fact]
        public void CreateProductionCountry_ShouldFail_WhenCountryNameIsNull()
        {
            var result = ProductionCountry.CreateProductionCountry(null!);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateProductionCountry_ShouldFail_WhenCountryNameIsEmpty()
        {
            var result = ProductionCountry.CreateProductionCountry("");

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateProductionCountry_ShouldFail_WhenCountryNameIsWhitespace()
        {
            var result = ProductionCountry.CreateProductionCountry("   ");

            Assert.True(result.IsFailure);
        }

        [Theory]
        [InlineData("United States")]
        [InlineData("United Kingdom")]
        [InlineData("France")]
        [InlineData("Germany")]
        [InlineData("Japan")]
        public void CreateProductionCountry_ShouldSucceed_WithVariousCountryNames(string countryName)
        {
            var result = ProductionCountry.CreateProductionCountry(countryName);

            Assert.True(result.IsSuccess);
            Assert.Equal(countryName, result.Value.Name);
        }

        [Fact]
        public void CreateProductionCountry_ShouldAssignUniqueIds()
        {
            var country1 = ProductionCountry.CreateProductionCountry("USA").Value;
            var country2 = ProductionCountry.CreateProductionCountry("UK").Value;

            Assert.NotEqual(country1.Id, country2.Id);
        }
    }
}
