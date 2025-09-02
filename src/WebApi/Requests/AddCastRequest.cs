namespace WebApi.Requests
{
    public sealed record AddCastRequest(
        Guid MovieRoleId,
        string? CharacterName)
    {
    }
}
