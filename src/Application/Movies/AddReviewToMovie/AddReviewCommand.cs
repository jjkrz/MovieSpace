using Application.Abstractions;

namespace Application.Movies.AddReviewToMovie
{
    public sealed record AddReviewCommand(
        Guid MovieId,
        string Content) : ICommand<AddReviewResponse>
    {
    }
}
