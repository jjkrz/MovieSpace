using Domain.Common;

namespace Domain.MoviePersonality
{
    public sealed class MovieRole: Entity
    { 
        public MovieRole(Guid id): base(id)
        {
        }

        public string RoleName { get; private set; } = null!;
        public string? CharacterName { get; private set; }
    }
}
