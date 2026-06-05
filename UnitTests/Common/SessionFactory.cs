using Domain.Sessions;

namespace MovieSpace.UnitTests.Common
{
    public static class SessionFactory
    {
        public static Session Create(
            string? displayName = null,
            string? accessCode = null)
        {
            return Session.CreateSession(
                displayName: displayName ?? "Test Session",
                accessCode: accessCode ?? "ACCESS123"
            ).Value;
        }
    }
}
