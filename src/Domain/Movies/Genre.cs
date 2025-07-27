using Domain.Common;

namespace Domain.Movies
{
    public sealed class Genre : Entity
    {
        public Genre(Guid Id) : base(Id)
        {
        }

        private List<Movie> Movies = [];
        public string Name { get; private set; } = null!;
    }
}
