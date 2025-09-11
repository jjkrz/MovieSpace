using Application.Common.ReadModels;
using Application.Genres.GetGenres;
using Application.ProductionCountries.GetProductionCountries;

namespace Application.Common.Dto
{
    public class MovieDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double AverageRating { get; set; }
        public int RatingCount { get; set; }
        public float ImdbRating { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public List<DirectorDto> Directors { get; set; } = null!;
        public List<ScriptWriterDto> ScriptWriters { get; set; } = null!;
        public List<ActorDto> Actors { get; set; } = null!;
        public List<GenreDto> Genres { get; set; } = null!;
        public List<ProductionCountryDto> ProductionCountries { get; set; } = null!;
        public List<ReviewReadModel> TopReviews { get; set; } = null!;

    }
}
