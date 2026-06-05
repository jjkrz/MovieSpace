using Domain.Movies;

namespace MovieSpace.UnitTests.Common
{
    public static class MovieFactory
    {
        public static Movie Create() => Movie.CreateMovie(
            title: "Example Movie",
            description: "This is an example movie used for testing purposes.",
            posterUri: new Uri("https://example.com/poster.jpg"),
            duration: new TimeOnly(2, 0, 0),
            releaseDate: new DateTime(2020, 1, 1)
        ).Value;
    }
}
