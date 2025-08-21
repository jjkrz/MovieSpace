using Domain.Common;

namespace Application.MovieRoles
{
    public static class MovieRolesApplicationErrors
    {
        public static Error MovieRoleAlreadyExists(string movieRole) => new Error(ErrorType.Conflict, $"Movie role with this name: {movieRole} already exists");
        public static Error MovieRoleNotFound(Guid id) => new Error(ErrorType.NotFound, $"The specified movie role with id: {id} was not found.");
        public static Error FailedToGetMovieRoles(string message) => new Error(ErrorType.Unexpected, $"Failed to get movie roles: {message}");
    }
}
