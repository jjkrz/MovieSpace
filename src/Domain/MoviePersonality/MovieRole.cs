using Domain.Common;

namespace Domain.MoviePersonality
{
    public sealed class MovieRole: Entity
    { 
        public string RoleName { get; private set; } = null!;
    }
}
