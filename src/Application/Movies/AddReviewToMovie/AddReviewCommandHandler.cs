using Application.Abstractions;
using Domain.Common;

namespace Application.Movies.AddReviewToMovie
{
    public class AddReviewCommandHandler : ICommandHandler<AddReviewCommand, AddReviewResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRepository _movieRepo;
        private readonly IUserContext _userContext;
        public AddReviewCommandHandler(IUnitOfWork unitOfWork, IMovieRepository movieRepo, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _movieRepo = movieRepo;
            _userContext = userContext;
        }
        public async Task<Result<AddReviewResponse>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentUserId();

            if (userId.IsFailure)
                return Result.Failure<AddReviewResponse>(userId.Error);

            var movie = _movieRepo.GetByIdWithReviewsAndRatingsByUserId(request.MovieId, userId.Value,  cancellationToken).Result;

            if (movie == null)
                return Result.Failure<AddReviewResponse>(Error.NotFound);

            var result = movie.AddReview(request.Content, userId.Value);

            if (result.IsFailure)
            {
                return Result.Failure<AddReviewResponse>(result.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new AddReviewResponse(request.MovieId, userId.Value, request.Content));
        }
    }
}
