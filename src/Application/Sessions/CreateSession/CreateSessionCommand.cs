using Application.Abstractions;

namespace Application.Sessions.CreateSession
{
    public sealed record CreateSessionCommand(
        string DisplayName) : ICommand<CreateSessionResponse>
    {
    }
}
