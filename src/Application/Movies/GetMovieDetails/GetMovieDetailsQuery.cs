using Application.Abstractions;
using Application.Common.Dto;

namespace Application.Movies.GetMovieDetails
{
    public sealed record GetMovieDetailsQuery(Guid MovieId)
        : IQuery<MovieDetailsDto>
    {
    }
}
