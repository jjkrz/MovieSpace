using Domain.Common;

namespace Application.Genres
{
    public static class GenreApplicationErrors
    {
        public static Error GenreAlreadyExists(string genreName) => 
            new Error(ErrorType.BadRequest,
            $"Genre with name '{genreName}' already exists."
        );

        public static Error FailedToGetGenres(string message) => 
            new Error(ErrorType.Unexpected, $"Failed to get genres: {message}");
    
        public static Error GenreNotFound(Guid id) =>
            new Error(ErrorType.NotFound, $"The specified genre with id: {id} was not found.");
    }
}
