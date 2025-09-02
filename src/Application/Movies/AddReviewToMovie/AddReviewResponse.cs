namespace Application.Movies.AddReviewToMovie
{
    public sealed record AddReviewResponse(
        Guid MovieId,
        Guid UserId,
        string Content)
    {
    }
}
