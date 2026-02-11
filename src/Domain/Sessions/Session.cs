using Domain.Common;
using Domain.Sessions.Errors;

namespace Domain.Sessions
{
    public class Session : Entity
    {
        private string AccessCode = null!;
        private ICollection<Participant> Participants = [];
        private Session() { }
        private Session(string displayName)
        {
            var participant = new Participant(displayName);
            this.AddToSession(displayName);
        }

        public void AddToSession(string displayName)
        {
            Participants.Add(new Participant(displayName));
        }

        public static Result<Session> CreateSession(string displayName)
        {
            if (displayName.Length > 30)
                return Result.Failure<Session>(SessionErrors.SessionDisplayNameLength);

            var session = new Session(displayName);

            return session;
        }
    }

    public class Participant
    {
        public string DisplayName { get; private set; }

        public Participant() { }
        public Participant(string displayName)  
        {
            DisplayName = displayName;
        }
    }
}
