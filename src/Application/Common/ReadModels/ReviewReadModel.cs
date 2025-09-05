namespace Application.Common.ReadModels
{
    public class ReviewReadModel
    {
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; } = default!;
        public string Content { get; set; } = default!;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
