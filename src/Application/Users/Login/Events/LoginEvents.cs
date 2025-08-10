using Application.Abstractions;

namespace Application.Users.Login.Events
{
    public sealed record UserLoginSuccessful: IApplicationEvent
    {
    }

    public sealed record UserLoginFailed : IApplicationEvent
    {
    }
}
