using Domain.Common;
using Domain.MoviePeople;
using Domain.MoviePersonality;
using Domain.Movies.Errors;

namespace Domain.Movies
{
    public sealed class Movie : Entity
    {
        private Movie()
        {
        }

        private Movie(string title, string description, Uri? posterUri, TimeOnly duration, DateTime releaseDate)
        {
            Title = title;
            Description = description;
            PosterUri = posterUri;
            Duration = duration;
            ReleaseDate = releaseDate;
        }

        private readonly List<Genre> _genres = [];
        public IReadOnlyCollection<Genre> Genres => _genres.AsReadOnly();


        private readonly List<MoviePersonRole> _moviePeople = [];
        public IReadOnlyCollection<MoviePersonRole> MoviePeople => _moviePeople.AsReadOnly();


        private readonly List<ProductionCountry> _productionCountries = [];
        public IReadOnlyCollection<ProductionCountry> ProductionCountries => _productionCountries.AsReadOnly();

        private readonly List<Rating> _ratings = [];
        public IReadOnlyCollection<Rating> Ratings => _ratings.AsReadOnly();

        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public Uri? PosterUri { get; private set; }
        public void SetPoster(string poster)
        {
            PosterUri = new Uri(poster, UriKind.Absolute);
        }
        public TimeOnly Duration { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public double? AverageRating { get; private set; }
        public int RatingCount { get; private set; }

        public static Result<Movie> CreateMovie(string title, string description, Uri? posterUri, TimeOnly duration, DateTime releaseDate)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Result.Failure<Movie>(Error.NullValue);

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Movie>(Error.NullValue);

            var movie = new Movie(title, description, posterUri, duration, releaseDate);

            return Result.Success(movie);
        }

        public Result Rate(Guid userId, int score)
        {
            var existingRating = _ratings.FirstOrDefault(r => r.UserId == userId && r.MovieId == Id);

            if (existingRating != null)
            {
                var updateResult = existingRating.UpdateScore(score);
                if (updateResult.IsFailure)
                    return Result.Failure(updateResult.Error);

                return Result.Success();
            }

            var newRating = Rating.Create(Id, userId, score);
            if (newRating.IsFailure)
                return Result.Failure<Rating>(newRating.Error);

            _ratings.Add(newRating.Value);
            return Result.Success(newRating.Value);
        }

        public void UpdateAverageRating(double? averageRating, int ratingCount)
        {
            AverageRating = averageRating;
            RatingCount = ratingCount;
        }

        public Result AddGenre(Genre genre)
        {
            if (genre == null)
                return Result.Failure(Error.NullValue);
            if (_genres.Any(g => g.Id == genre.Id))
                return Result.Failure(MovieErrors.DuplicateGenre(genre.Name));
            _genres.Add(genre);
            return Result.Success();
        }

        public Result AddCastMember(MoviePerson person, MovieRole role, string? characterName)
        {
            if ((role.RoleName == "Aktor" || role.RoleName == "Actor") && characterName == null)
                return Result.Failure(MovieErrors.NullCharacterName);

            if ((role.RoleName != "Aktor" || role.RoleName != "Actor") && characterName != null)
                characterName = null;

            if (_moviePeople.Any(mp => mp.MovieId == Id && mp.MoviePersonId == person.Id && mp.MovieRoleId == role.Id))
            {
                return Result.Failure(MovieErrors.DuplicateCastMember);
            }

            var moviePersonRole = new MoviePersonRole(this.Id, person.Id, role.Id, characterName);

            _moviePeople.Add(moviePersonRole);

            return Result.Success();
        }
    }
}
