using Domain.Common;

namespace Application.Movies
{
    public static class MovieApplicationErrors
    {
        public static Error FailedToGetMovies =>
            new Error(ErrorType.Unexpected, "An error occurred while retrieving movies. Please try again later.");
    }
}
