using System.Diagnostics.CodeAnalysis;
using Domain.Actors;
using Domain.Common;

namespace Domain.Movies
{
    public sealed class Movie : Entity
    {
        public Movie(Guid Id) : base(Id)
        {
        }

        private readonly List<Genre> Genres = [];
        private readonly List<MoviePerson> Actors = [];
        private readonly List<ProductionCountry> ProductionCountry = [];
        private readonly List<Rating> ratings = [];
        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public Uri? PosterUri { get; private set; }
        public void SetPoster(string poster)
        {
            PosterUri = new Uri(poster, UriKind.Absolute);
        }
        public MoviePerson Author { get; private set; } = null!;
        public MoviePerson Director { get; private set; } = null!;
        public TimeOnly Duration { get; private set; }
        public DateTime ReleaseDate { get; private set; }

    }
}
