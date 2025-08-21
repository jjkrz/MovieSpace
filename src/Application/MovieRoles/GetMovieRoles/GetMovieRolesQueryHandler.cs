using Application.Abstractions;
using Application.Helpers;
using AutoMapper;
using Domain.Common;

namespace Application.MovieRoles.GetMovieRoles
{
    public class GetMovieRolesQueryHandler : IQueryHandler<GetMovieRolesQuery, PaginatedResponse<RoleDto>>
    {
        private readonly IMovieRoleRepository _repository;
        private readonly IMapper _mapper;

        public GetMovieRolesQueryHandler(IMovieRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResponse<RoleDto>>> Handle(GetMovieRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = await _repository.GetPaginatedAsync(request.Page, request.PageSize, request.Search, cancellationToken);
                var dto = _mapper.Map<List<RoleDto>>(roles);
                var total = await _repository.GetCountAsync(cancellationToken);
                var pageInfo = new PageInfo(request.Page, request.PageSize, total);
                return Result.Success(new PaginatedResponse<RoleDto>(dto, pageInfo));
            }
            catch (Exception ex)
            {
                return Result.Failure<PaginatedResponse<RoleDto>>(MovieRolesApplicationErrors.FailedToGetMovieRoles(ex.Message));
            }
        }
    }
}


