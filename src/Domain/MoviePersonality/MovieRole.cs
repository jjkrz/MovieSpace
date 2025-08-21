using Domain.Common;

namespace Domain.MoviePersonality
{
    public sealed class MovieRole: Entity
    {
        private MovieRole()
        {
        }

        private MovieRole(string roleName)
        {
            RoleName = roleName;
        }

        public string RoleName { get; private set; } = null!;

        public static Result<MovieRole> Create(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return Result.Failure<MovieRole>(Error.NullValue);
            }

            return new MovieRole(roleName);
        }

        public Result Update(string? roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return Result.Failure(Error.NullValue);
            }

            RoleName = roleName!;
            return Result.Success();
        }
    }
}
