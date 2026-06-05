using Domain.Common;

namespace Infrastructure.Sockets
{
    public class SwipeQueue
    {
        // sessionid -> movieid -> userid -> swipe
        private readonly Dictionary<Guid, Dictionary<Guid, Dictionary<Guid, SwipeDirection>>> _queue = new();

        public Guid? Swipe(Guid sessionId, Guid movieId, Guid userId, SwipeDirection swipe, int totalParticipants)
        {
            if (!_queue.ContainsKey(sessionId))
                _queue[sessionId] = new();

            if (!_queue[sessionId].ContainsKey(movieId)) 
                _queue[sessionId][movieId] = new();

            _queue[sessionId][movieId][userId] = swipe;

            return IsMatch(sessionId, movieId, totalParticipants);
        }

        private Guid? IsMatch(Guid sessionId, Guid movieId, int totalParticipants)
        {
            var swipes = _queue[sessionId][movieId];

            if (swipes.Count == totalParticipants && swipes.Values.All(s => s == SwipeDirection.Accept))
                return movieId;

            return null;
        }

        public void ClearSession(Guid sessionId) => _queue.Remove(sessionId);
    }

    public enum SwipeDirection
    {
        Accept,
        Reject
    }
}
