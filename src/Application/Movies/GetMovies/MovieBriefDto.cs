namespace Application.Movies.GetMovies
{
    public class MovieBriefDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genres { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }
        public double AverageRating { get; set; }
    }
}
