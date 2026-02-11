namespace Application.Sessions.CreateSession
{
    public sealed record CreateSessionResponse(
        Guid Id, string AccessKey)
    {
    }
}
