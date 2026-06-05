using Domain.Common;

namespace Domain.Sessions.Errors
{
    public class SessionErrors
    {
        public static Error SessionDisplayNameLength => new Error(ErrorType.Validation, "Display name provided for session is too long");
    }
}
