namespace Application.Services
{
    public record MovieRatingUpdate(Guid MovieId, double? AverageRating, int RatingCount)
    {
    }
}
