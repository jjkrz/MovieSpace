using Application.Abstractions;
using Application.Helpers;

namespace Application.Genres.GetGenres
{
    public record GetGenresQuery(int Page, int PageSize) : IQuery<PaginatedResponse<GenreDto>>
    {
    }
}
