using Domain.Common;

namespace Application.Movies
{
    public static class MovieApplicationErrors
    {
        public static Error FailedToGetMovies =>
            new Error(ErrorType.Unexpected, "An error occurred while retrieving movies. Please try again later.");

        public static Error FailedToGetMovieDetails =>
            new Error(ErrorType.Unexpected, "An error occurred while retrieving movie details. Please try again later.");

        public static Error GenreNotFound => 
            new Error(ErrorType.NotFound, "The specified genre was not found.");

        public static Error MovieNotFound =>
            new Error(ErrorType.NotFound, "The specified movie was not found.");
    }
}
