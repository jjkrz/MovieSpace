using Application.Abstractions;

namespace Application.Movies.AddProductionCountryToMovie
{
    public sealed record AddProductionCountryToMovieCommand(
        Guid MovieId,
        Guid ProductionCountryId) : ICommand
    {
    }
}
