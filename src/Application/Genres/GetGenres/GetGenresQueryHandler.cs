using Application.Abstractions;
using Application.Helpers;
using AutoMapper;
using Domain.Common;
using Domain.Movies;

namespace Application.Genres.GetGenres
{
    public class GetGenresQueryHandler : IQueryHandler<GetGenresQuery, PaginatedResponse<GenreDto>>
    {
        private readonly IRepository<Genre> _genreRepo;
        private readonly IMapper _mapper;
        public GetGenresQueryHandler(IRepository<Genre> genreRepo, IMapper mapper)
        {
            _genreRepo = genreRepo;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResponse<GenreDto>>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var genres = await _genreRepo.GetAllAsync(cancellationToken: cancellationToken);
                
                var filteredGenres = genres
                    .Skip(request.Page * request.PageSize)
                    .Take(request.PageSize);

                var genresDto = _mapper.Map<List<GenreDto>>(filteredGenres);

                var pageInfo = new PageInfo(request.Page, request.PageSize, genres.Count);

                return Result.Success(new PaginatedResponse<GenreDto>(genresDto, pageInfo));
            }
            catch (Exception ex)
            {
                return Result.Failure<PaginatedResponse<GenreDto>>(GenreApplicationErrors.FailedToGetGenres(ex.Message));

            }
        }
    }
}
