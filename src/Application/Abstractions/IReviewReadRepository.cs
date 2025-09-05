using Application.Common.ReadModels;

namespace Application.Abstractions
{
    public interface IReviewReadRepository
    {
        Task<List<ReviewReadModel>> GetRecentReviewsForMovieAsync(Guid movieId, int count);
    }
}
