using Application.Abstractions;
using Application.Helpers;
using AutoMapper;
using Domain.Common;

namespace Application.Movies.GetMovies
{
    public class GetMoviesQueryHandler : IQueryHandler<GetMoviesQuery, PaginatedResponse<MovieBriefDto>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public GetMoviesQueryHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResponse<MovieBriefDto>>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var movies = await _movieRepository.GetMoviesAsync(request.Page, request.PageSize, cancellationToken: cancellationToken);
                
                var moviesCount = await _movieRepository.GetMoviesCountAsync(cancellationToken: cancellationToken);

                var pageInfo = new PageInfo(request.Page, request.PageSize, moviesCount);

                var movieDtos = _mapper.Map<List<MovieBriefDto>>(movies);

                var result = new PaginatedResponse<MovieBriefDto>(movieDtos, pageInfo);

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure<PaginatedResponse<MovieBriefDto>>(MovieApplicationErrors.FailedToGetMovies);
            }
        }
    }
}
