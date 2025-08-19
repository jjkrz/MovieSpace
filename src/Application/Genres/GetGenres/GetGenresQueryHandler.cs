using Application.Abstractions;
using Application.Helpers;
using AutoMapper;
using Domain.Common;

namespace Application.Genres.GetGenres
{
    public class GetGenresQueryHandler : IQueryHandler<GetGenresQuery, PaginatedResponse<GenreDto>>
    {
        private readonly IGenreRepository _genreRepo;
        private readonly IMapper _mapper;
        public GetGenresQueryHandler(IGenreRepository genreRepo, IMapper mapper)
        {
            _genreRepo = genreRepo;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResponse<GenreDto>>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var filteredGenres = await _genreRepo.GetPaginatedAsync(request.Page, request.PageSize, cancellationToken: cancellationToken);
                
                var genresDto = _mapper.Map<List<GenreDto>>(filteredGenres);

                var genresCount = await _genreRepo.GetCountAsync(cancellationToken: cancellationToken);

                var pageInfo = new PageInfo(request.Page, request.PageSize, genresCount);

                return Result.Success(new PaginatedResponse<GenreDto>(genresDto, pageInfo));
            }
            catch (Exception ex)
            {
                return Result.Failure<PaginatedResponse<GenreDto>>(GenreApplicationErrors.FailedToGetGenres(ex.Message));

            }
        }
    }
}
