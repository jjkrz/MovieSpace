using Application.Abstractions;
using Application.Common.Dto;
using AutoMapper;
using Domain.Common;
using Domain.Movies;

namespace Application.Movies.GetMovieDetails
{
    public class GetMovieDetailsQueryHandler : IQueryHandler<GetMovieDetailsQuery, MovieDetailsDto>
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IReviewReadRepository _reviewReadRepo;
        private readonly IMapper _mapper;

        public GetMovieDetailsQueryHandler(IMovieRepository movieRepo, IMapper mapper, IReviewReadRepository reviewReadRepo )
        {
            _movieRepo = movieRepo;
            _mapper = mapper;
            _reviewReadRepo = reviewReadRepo;

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
                var dto = _mapper.Map<MovieDetailsDto>(movie);

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
