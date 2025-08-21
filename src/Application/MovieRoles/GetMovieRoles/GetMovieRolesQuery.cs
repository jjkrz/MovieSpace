using Application.Abstractions;
using Application.Helpers;

namespace Application.MovieRoles.GetMovieRoles
{
    public record GetMovieRolesQuery(int Page, int PageSize, string? Search = null) : IQuery<PaginatedResponse<RoleDto>>
    {
    }
}


