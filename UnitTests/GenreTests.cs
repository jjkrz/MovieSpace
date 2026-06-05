using Domain.Movies;

namespace MovieSpace.UnitTests
{
    public class GenreTests
    {
        [Fact]
        public void CreateGenre_ShouldSucceed_WhenNameIsValid()
        {
            var result = Genre.CreateGenre("Action");

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal("Action", result.Value.Name);
        }

        [Fact]
        public void CreateGenre_ShouldFail_WhenNameIsNull()
        {
            var result = Genre.CreateGenre(null!);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateGenre_ShouldFail_WhenNameIsEmpty()
        {
            var result = Genre.CreateGenre("");

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateGenre_ShouldFail_WhenNameIsWhitespace()
        {
            var result = Genre.CreateGenre("   ");

            Assert.True(result.IsFailure);
        }

        [Theory]
        [InlineData("Comedy")]
        [InlineData("Drama")]
        [InlineData("Horror")]
        [InlineData("Science Fiction")]
        [InlineData("Animation")]
        public void CreateGenre_ShouldSucceed_WithVariousGenreNames(string genreName)
        {
            var result = Genre.CreateGenre(genreName);

            Assert.True(result.IsSuccess);
            Assert.Equal(genreName, result.Value.Name);
        }

        [Fact]
        public void CreateGenre_ShouldAssignUniqueId()
        {
            var result1 = Genre.CreateGenre("Action");
            var result2 = Genre.CreateGenre("Comedy");

            Assert.NotEqual(result1.Value.Id, result2.Value.Id);
        }
    }
}
