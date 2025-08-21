using Application.Abstractions;
using Application.Helpers;

namespace Application.MoviePeople.GetMoviePeople
{
    public record GetMoviePeopleQuery(int Page, int PageSize, string? Search = null) : IQuery<PaginatedResponse<MoviePersonDto>>
    {
    }
}

