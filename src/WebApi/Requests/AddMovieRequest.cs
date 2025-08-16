namespace WebApi.Requests
{
    public class AddMovieRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Uri? PosterUri { get; set; }
        public TimeOnly Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
