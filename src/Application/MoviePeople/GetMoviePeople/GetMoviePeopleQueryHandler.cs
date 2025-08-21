using Application.Abstractions;
using Application.Helpers;
using AutoMapper;
using Domain.Common;

namespace Application.MoviePeople.GetMoviePeople
{
    public class GetMoviePeopleQueryHandler : IQueryHandler<GetMoviePeopleQuery, PaginatedResponse<MoviePersonDto>>
    {
        private readonly IMoviePersonRepository _repository;
        private readonly IMapper _mapper;

        public GetMoviePeopleQueryHandler(IMoviePersonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResponse<MoviePersonDto>>> Handle(GetMoviePeopleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var people = await _repository.GetPaginatedAsync(request.Page, request.PageSize, request.Search, cancellationToken);
                var dto = _mapper.Map<List<MoviePersonDto>>(people);
                var total = await _repository.GetCountAsync(cancellationToken);
                var pageInfo = new PageInfo(request.Page, request.PageSize, total);
                return Result.Success(new PaginatedResponse<MoviePersonDto>(dto, pageInfo));
            }
            catch (Exception ex)
            {
                return Result.Failure<PaginatedResponse<MoviePersonDto>>(new Error(ErrorType.Unexpected, $"Failed to get movie people: {ex.Message}"));
            }
        }
    }
}

