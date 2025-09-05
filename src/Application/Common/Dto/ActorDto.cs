namespace Application.Common.Dto
{
    public sealed record ActorDto(
        Guid Id,
        string FullName,
        string CharacterName)
    {
    }
}