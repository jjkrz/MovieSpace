using Domain.Movies;
using MovieSpace.UnitTests.Common;

namespace MovieSpace.UnitTests
{
    public class ReviewTests
    {
        [Fact]
        public void Review_ShouldBeCreated_WithValidData()
        {
            var movieId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var rating = 8;
            var content = "Excellent movie!";

            var review = new Review(movieId, userId, rating, content);

            Assert.Equal(movieId, review.MovieId);
            Assert.Equal(userId, review.UserId);
            Assert.Equal(rating, review.Rating);
            Assert.Equal(content, review.Content);
            Assert.NotEqual(default, review.CreatedAt);
        }

        [Fact]
        public void Review_CreatedAt_ShouldBeSetToCurrentTime()
        {
            var review = ReviewFactory.Create();
            var now = DateTime.UtcNow;

            Assert.True(review.CreatedAt <= now);
            Assert.True(review.CreatedAt.AddSeconds(5) >= now);
        }

        [Fact]
        public void UpdateContent_ShouldUpdateContentAndRating()
        {
            var review = ReviewFactory.Create(rating: 5, content: "Not great");
            var newContent = "Actually, it's amazing!";
            var newRating = 9;

            review.UpdateContent(newContent, newRating);

            Assert.Equal(newContent, review.Content);
            Assert.Equal(newRating, review.Rating);
        }

        [Fact]
        public void UpdateContent_MultipleTimes_ShouldUpdateTimestampEachTime()
        {
            var review = ReviewFactory.Create();
            review.UpdateContent("First update", 8);
            var firstUpdateTime = review.UpdatedAt;

            System.Threading.Thread.Sleep(100);

            review.UpdateContent("Second update", 9);
            var secondUpdateTime = review.UpdatedAt;

            Assert.True(secondUpdateTime > firstUpdateTime);
        }

        [Fact]
        public void Review_ShouldHandleEmptyContent()
        {
            var review = new Review(Guid.NewGuid(), Guid.NewGuid(), 5, "");

            Assert.Equal("", review.Content);
        }

        [Fact]
        public void Review_ShouldHandleMaxRating()
        {
            var review = ReviewFactory.Create(rating: 10);

            Assert.Equal(10, review.Rating);
        }

        [Fact]
        public void Review_ShouldHandleMinRating()
        {
            var review = ReviewFactory.Create(rating: 1);

            Assert.Equal(1, review.Rating);
        }
    }
}
