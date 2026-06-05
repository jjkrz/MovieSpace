using Domain.Movies;

namespace MovieSpace.UnitTests.Common
{
    public static class RatingFactory
    {
        public static Rating Create(
            Guid? movieId = null,
            Guid? userId = null,
            int score = 8)
        {
            var result = Rating.Create(
                movieId: movieId ?? Guid.NewGuid(),
                userId: userId ?? Guid.NewGuid(),
                score: score
            );

            return result.Value;
        }
    }
}
