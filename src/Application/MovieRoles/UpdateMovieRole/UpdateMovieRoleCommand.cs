using Application.Abstractions;

namespace Application.MovieRoles.UpdateMovieRole
{
    public sealed record UpdateMovieRoleCommand(Guid Id, string RoleName) : ICommand
    {
    }
}


