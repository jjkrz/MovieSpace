namespace Application.Movies.AddCastToMovie
{
    public sealed record AddCastResponse(
        string MovieTitle,
        string CastFullName,
        string RoleName,
        string? CharacterName)
    {
    }
}
