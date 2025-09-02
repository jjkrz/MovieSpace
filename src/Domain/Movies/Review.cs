using Domain.Common;

namespace Domain.Movies
{
    public sealed class Review
    {
        private Review() { }

        public Review(Guid movieId, Guid userId, int rating, string content)
        {
            MovieId = movieId;
            UserId = userId;
            Rating = rating;
            Content = content;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid MovieId { get; private set; }
        public Guid UserId { get; private set; }
        public int Rating { get; private set; }        
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public void UpdateContent(string content, int rating)
        {
            Content = content;
            Rating = rating;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
