using Application.Abstractions;

namespace Application.MovieRoles.DeleteMovieRole
{
    public sealed record DeleteMovieRoleCommand(Guid Id) : ICommand
    {
    }
}


