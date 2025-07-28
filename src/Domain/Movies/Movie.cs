using Domain.Common;
using Domain.MoviePersonality;

namespace Domain.Movies
{
    public sealed class Movie : Entity
    {
        public Movie(Guid Id) : base(Id)
        {
        }

        private readonly List<Genre> Genres = [];
        private readonly List<MoviePersonRole> MoviePeople = [];
        private readonly List<ProductionCountry> ProductionCountries = [];
        private readonly List<Rating> Ratings = [];
        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public Uri? PosterUri { get; private set; }
        public void SetPoster(string poster)
        {
            PosterUri = new Uri(poster, UriKind.Absolute);
        }
        public TimeOnly Duration { get; private set; }
        public DateTime ReleaseDate { get; private set; }

    }
}
