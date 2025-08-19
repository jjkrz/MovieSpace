namespace Application.Services
{
    public record MovieRatingData(Guid MovieId, double? AverageRating, int RatingCount)
    {
    }
}
