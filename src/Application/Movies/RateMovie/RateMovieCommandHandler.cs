using Application.Abstractions;
using Domain.Common;

namespace Application.Movies.RateMovie
{
    public class RateMovieCommandHandler : ICommandHandler<RateMovieCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRepository _movieRepo;
        private readonly IUserContext _userContext;

        public RateMovieCommandHandler(IUnitOfWork unitOfWork, IMovieRepository movieRepo, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _movieRepo = movieRepo;
            _userContext = userContext;
        }

        public async Task<Result> Handle(RateMovieCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentUserId();

            if (userId.IsFailure)
                return Result.Failure(userId.Error);

            var movie = await _movieRepo.GetByIdWithRatingAsync(request.MovieId, cancellationToken: cancellationToken);

            if (movie == null)
                return Result.Failure(Error.NotFound);

            var result = movie.Rate(userId.Value, request.Score);

            if (result.IsFailure)
                return Result.Failure(result.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
