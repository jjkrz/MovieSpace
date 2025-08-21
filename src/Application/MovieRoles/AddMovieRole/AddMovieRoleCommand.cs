using Application.Abstractions;

namespace Application.MovieRoles.AddMovieRole
{
    public sealed record AddMovieRoleCommand(string RoleName): ICommand<AddMovieRoleResponse>
    {
    }
}
