using Application.Abstractions;
using Application.Helpers;

namespace Application.Movies.GetMovies
{
    public record GetMoviesQuery(int Page, int PageSize): IQuery<PaginatedResponse<MovieBriefDto>>
    {
    }
}
