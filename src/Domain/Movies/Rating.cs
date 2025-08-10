namespace Domain.Movies
{
    public sealed class Rating
    {
        public Rating(Guid movieId, Guid userId, int score)
        {
            MovieId = movieId;
            UserId = userId;
            Score = score;
        }

        public Guid MovieId { get; private set; }
        public Guid UserId { get; private set; }

        public int Score { get; private set; }

        public Movie Movie { get; private set; } = null!;
    }
}