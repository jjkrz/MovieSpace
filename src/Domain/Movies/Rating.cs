using Domain.Common;
using Domain.Movies.Errors;

namespace Domain.Movies
{
    public sealed class Rating
    {
        private Rating()
        {
        }

        private Rating(Guid movieId, Guid userId, int score)
        {
            MovieId = movieId;
            UserId = userId;
            Score = score;
        }

        public Guid MovieId { get; private set; }
        public Guid UserId { get; private set; }

        public int Score { get; private set; }

        public Movie Movie { get; private set; } = null!;

        public static Result<Rating> Create(Guid movieId, Guid userId, int score)
        {
            if (score < 1 || score > 10)
                return Result.Failure<Rating>(MovieErrors.RatingScoreOutOfRange(score));

            if (movieId == Guid.Empty || userId == Guid.Empty)
                return Result.Failure<Rating>(Error.NullValue);

            return Result.Success(new Rating(movieId, userId, score));
        }

        public Result UpdateScore(int newScore)
        {
            if (newScore < 1 || newScore > 10)
                return Result.Failure(MovieErrors.RatingScoreOutOfRange(newScore));

            Score = newScore;
            return Result.Success();
        }
    }
}