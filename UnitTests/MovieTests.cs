using Domain.Movies;
using MovieSpace.UnitTests.Common;

namespace MovieSpace.UnitTests
{
    public class MovieTests
    {
        [Fact]
        public void MovieCreate_ShoudFail_WhenTitleIsEmtpy()
        {
            var result = Movie.CreateMovie("", "Some description", null, TimeOnly.MinValue, DateTime.Now);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void MovieCreate_ShouldSucceed_WhenAllParametersAreValid()
        {
            var result = Movie.CreateMovie("Some title", "Some description", null, TimeOnly.MinValue, DateTime.Now);
            
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void MovieCreate_ShouldSucceed_WhenPosterUriIsNull()
        {
            var result = Movie.CreateMovie("Some title", "Some description", null, TimeOnly.MinValue, DateTime.Now);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Rate_ShouldAddNewRating()
        {
            var movie = MovieFactory.Create();

            var result = movie.Rate(Guid.NewGuid(), 5);

            Assert.Single(movie.Ratings);
            Assert.Equal(5, movie.Ratings.First().Score);
        }

        [Fact]
        public void Rate_ShouldUpdateExistingRating()
        {
            var movie = MovieFactory.Create();
            var userId = Guid.NewGuid();
            
            movie.Rate(userId, 5);
            var result = movie.Rate(userId, 3);
            
            Assert.Single(movie.Ratings);
            Assert.Equal(3, movie.Ratings.First().Score);
        }

        [Fact]
        public void AddGenre_ShouldAddGenreToMovie()
        {
            var movie = MovieFactory.Create();
            var genre = GenreFactory.Create();

            var result = movie.AddGenre(genre);

            Assert.Single(movie.Genres);
            Assert.Equal("Action", movie.Genres.First().Name);
        }
}
}   
