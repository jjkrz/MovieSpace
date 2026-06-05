using Domain.Movies;

namespace MovieSpace.UnitTests.Common
{
    public static class ReviewFactory
    {
        public static Review Create(
            Guid? movieId = null,
            Guid? userId = null,
            int rating = 8,
            string? content = null)
        {
            return new Review(
                movieId: movieId ?? Guid.NewGuid(),
                userId: userId ?? Guid.NewGuid(),
                rating: rating,
                content: content ?? "Great movie! Highly recommended."
            );
        }
    }
}
