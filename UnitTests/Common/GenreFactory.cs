using Domain.Movies;

namespace MovieSpace.UnitTests.Common
{
    public static class GenreFactory
    {
        public static Genre Create() => Genre.CreateGenre("Action").Value;
    }
}
