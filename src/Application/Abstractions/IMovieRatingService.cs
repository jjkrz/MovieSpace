using Domain.Common;

namespace Application.Abstractions
{
    public interface IMovieRatingService
    {
        Task<Result<float>> GetImdbMovieRatingAsync(string title, int year);

    }
}
