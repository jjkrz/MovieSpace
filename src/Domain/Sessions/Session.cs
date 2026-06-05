using Domain.Common;
using Domain.Sessions.Errors;

namespace Domain.Sessions
{
    public class Session : Entity
    {
        private readonly List<Participant> _participants = [];
        public IReadOnlyCollection<Participant> Participants => _participants.AsReadOnly();

        public string AccessCode = null!;

        private Session() { }
        private Session(string displayName, string accessCode)
        {
            this.AddToSession(displayName);
            AccessCode = accessCode;
        }

        public void AddToSession(string displayName)
        {
            _participants.Add(new Participant(displayName));
        }

        public static Result<Session> CreateSession(string displayName, string accessCode)
        {
            if (displayName.Length > 30)
                return Result.Failure<Session>(SessionErrors.SessionDisplayNameLength);

            var session = new Session(displayName, accessCode);

            return session;
        }
    }

    public class Participant : Entity
    {
        public string DisplayName { get; private set; }

        public Participant() { }
        public Participant(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
