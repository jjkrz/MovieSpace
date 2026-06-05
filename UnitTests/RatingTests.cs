using Domain.Movies;
using MovieSpace.UnitTests.Common;

namespace MovieSpace.UnitTests
{
    public class RatingTests
    {
        [Fact]
        public void Rating_Create_ShouldSucceed_WithValidParameters()
        {
            var movieId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var score = 8;

            var result = Rating.Create(movieId, userId, score);

            Assert.True(result.IsSuccess);
            Assert.Equal(movieId, result.Value.MovieId);
            Assert.Equal(userId, result.Value.UserId);
            Assert.Equal(score, result.Value.Score);
        }

        [Fact]
        public void Rating_Create_ShouldFail_WhenScoreIsBelow1()
        {
            var result = Rating.Create(Guid.NewGuid(), Guid.NewGuid(), 0);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Rating_Create_ShouldFail_WhenScoreIsAbove10()
        {
            var result = Rating.Create(Guid.NewGuid(), Guid.NewGuid(), 11);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Rating_Create_ShouldFail_WhenMovieIdIsEmpty()
        {
            var result = Rating.Create(Guid.Empty, Guid.NewGuid(), 5);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Rating_Create_ShouldFail_WhenUserIdIsEmpty()
        {
            var result = Rating.Create(Guid.NewGuid(), Guid.Empty, 5);

            Assert.True(result.IsFailure);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void Rating_Create_ShouldSucceed_WithValidScores(int score)
        {
            var result = Rating.Create(Guid.NewGuid(), Guid.NewGuid(), score);

            Assert.True(result.IsSuccess);
            Assert.Equal(score, result.Value.Score);
        }

        [Fact]
        public void Rating_UpdateScore_ShouldSucceed_WithValidScore()
        {
            var rating = RatingFactory.Create(score: 5);

            var result = rating.UpdateScore(8);

            Assert.True(result.IsSuccess);
            Assert.Equal(8, rating.Score);
        }

        [Fact]
        public void Rating_UpdateScore_ShouldFail_WhenScoreIsBelow1()
        {
            var rating = RatingFactory.Create();

            var result = rating.UpdateScore(0);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Rating_UpdateScore_ShouldFail_WhenScoreIsAbove10()
        {
            var rating = RatingFactory.Create();

            var result = rating.UpdateScore(11);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Rating_UpdateScore_ShouldUpdateTimestamp()
        {
            var rating = RatingFactory.Create();
            var originalUpdateTime = rating.UpdatedAt;

            System.Threading.Thread.Sleep(100);
            rating.UpdateScore(9);

            Assert.True(rating.UpdatedAt > originalUpdateTime);
        }

        [Fact]
        public void Rating_Create_ShouldSetCreatedAtTimestamp()
        {
            var beforeCreation = DateTime.UtcNow;
            var result = Rating.Create(Guid.NewGuid(), Guid.NewGuid(), 5);
            var afterCreation = DateTime.UtcNow;

            Assert.True(result.Value.CreatedAt >= beforeCreation);
            Assert.True(result.Value.CreatedAt <= afterCreation);
        }

        [Fact]
        public void Rating_UpdateScore_ShouldNotChangeMovieOrUserId()
        {
            var movieId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var rating = RatingFactory.Create(movieId: movieId, userId: userId, score: 5);

            rating.UpdateScore(9);

            Assert.Equal(movieId, rating.MovieId);
            Assert.Equal(userId, rating.UserId);
        }
    }
}
