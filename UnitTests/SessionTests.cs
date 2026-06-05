using Domain.Sessions;
using MovieSpace.UnitTests.Common;

namespace MovieSpace.UnitTests
{
    public class SessionTests
    {
        [Fact]
        public void CreateSession_ShouldSucceed_WithValidParameters()
        {
            var displayName = "Test Session";
            var accessCode = "ACCESS123";

            var result = Session.CreateSession(displayName, accessCode);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void CreateSession_ShouldFail_WhenDisplayNameExceeds30Characters()
        {
            var longName = new string('a', 31);
            var result = Session.CreateSession(longName, "CODE123");

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateSession_ShouldSucceed_WhenDisplayNameIs30Characters()
        {
            var name30Chars = new string('a', 30);
            var result = Session.CreateSession(name30Chars, "CODE123");

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void CreateSession_ShouldInitializeParticipantsWithCreator()
        {
            var displayName = "Creator Name";
            var result = Session.CreateSession(displayName, "CODE123");

            Assert.Single(result.Value.Participants);
            Assert.Equal(displayName, result.Value.Participants.First().DisplayName);
        }

        [Fact]
        public void CreateSession_ShouldSetAccessCode()
        {
            var accessCode = "SECRET456";
            var result = Session.CreateSession("Test", accessCode);

            Assert.Equal(accessCode, result.Value.AccessCode);
        }

        [Fact]
        public void AddToSession_ShouldAddParticipant()
        {
            var session = SessionFactory.Create();
            var newDisplayName = "New Participant";

            session.AddToSession(newDisplayName);

            Assert.Equal(2, session.Participants.Count);
            Assert.True(session.Participants.Any(p => p.DisplayName == newDisplayName));
        }

        [Fact]
        public void AddToSession_MultipleParticipants_ShouldAddAll()
        {
            var session = SessionFactory.Create();

            session.AddToSession("Participant 2");
            session.AddToSession("Participant 3");
            session.AddToSession("Participant 4");

            Assert.Equal(4, session.Participants.Count);
        }

        [Fact]
        public void AddToSession_ShouldAssignUniqueIdToEachParticipant()
        {
            var session = SessionFactory.Create();
            
            session.AddToSession("Participant 2");
            session.AddToSession("Participant 3");

            var participantIds = session.Participants.Select(p => p.Id).ToList();
            var uniqueIds = participantIds.Distinct().Count();

            Assert.Equal(participantIds.Count, uniqueIds);
        }

        [Fact]
        public void Session_ParticipantsCollection_ShouldBeReadOnly()
        {
            var session = SessionFactory.Create();
            var participants = session.Participants;

            // This should throw because the collection is read-only
            var exception = Record.Exception(() => ((List<Participant>)participants).Add(new Participant("New")));

            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData("Alice")]
        [InlineData("Bob")]
        [InlineData("Test User")]
        [InlineData("1")]
        public void AddToSession_ShouldAcceptVariousDisplayNames(string displayName)
        {
            var session = SessionFactory.Create();

            session.AddToSession(displayName);

            Assert.Contains(session.Participants, p => p.DisplayName == displayName);
        }

        [Fact]
        public void CreateSession_ShouldAssignUniqueSessionId()
        {
            var session1 = SessionFactory.Create();
            var session2 = SessionFactory.Create();

            Assert.NotEqual(session1.Id, session2.Id);
        }
    }
}
