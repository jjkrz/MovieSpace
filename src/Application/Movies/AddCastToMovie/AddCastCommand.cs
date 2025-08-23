using Application.Abstractions;

namespace Application.Movies.AddCastToMovie
{
    public sealed record AddCastCommand(Guid MovieId, Guid MoviePersonId, Guid MovieRoleId, string? CharacterName) : ICommand<AddCastResponse>
    {
    }
}
