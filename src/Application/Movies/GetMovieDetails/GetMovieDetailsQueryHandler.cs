using Application.Abstractions;
using Application.Common.Dto;
using AutoMapper;
using Domain.Common;

namespace Application.Movies.GetMovieDetails
{
    public class GetMovieDetailsQueryHandler : IQueryHandler<GetMovieDetailsQuery, MovieDetailsDto>
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IReviewReadRepository _reviewReadRepo;
        private readonly IMapper _mapper;
        private readonly IMovieRatingService _movieRatingService;

        public GetMovieDetailsQueryHandler(
            IMovieRepository movieRepo, 
            IMapper mapper, 
            IReviewReadRepository reviewReadRepo,
            IMovieRatingService movieRatingService)
        {
            _movieRepo = movieRepo;
            _mapper = mapper;
            _reviewReadRepo = reviewReadRepo;
            _movieRatingService = movieRatingService;
        }
        public async Task<Result<MovieDetailsDto>> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var movie = await _movieRepo.GetByIdAsync(request.MovieId, cancellationToken);

                if (movie == null)
                {
                    return Result.Failure<MovieDetailsDto>(MovieApplicationErrors.MovieNotFound);
                }

                var imdbRating = await _movieRatingService.GetImdbMovieRatingAsync(movie.Title, movie.ReleaseDateYear);

                if (imdbRating.IsFailure)
                    return Result.Failure<MovieDetailsDto>(imdbRating.Error);

                var dto = _mapper.Map<MovieDetailsDto>(movie);

                dto.ImdbRating = imdbRating.Value;
                dto.TopReviews = await _reviewReadRepo.GetRecentReviewsForMovieAsync(request.MovieId, 5);

                return Result.Success(dto);
            }
            catch (Exception)
            {
                return Result.Failure<MovieDetailsDto>(MovieApplicationErrors.FailedToGetMovieDetails);
            }
        }
    }
}
