using Application.Abstractions;
using Application.Helpers;
using AutoMapper;
using Domain.Common;

namespace Application.ProductionCountries.GetProductionCountries
{
    public class GetProductionCountriesQueryHandler : IQueryHandler<GetProductionCountriesQuery, PaginatedResponse<ProductionCountryDto>>
    {
        private readonly IProductionCountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetProductionCountriesQueryHandler(IProductionCountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResponse<ProductionCountryDto>>> Handle(GetProductionCountriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var filtered = await _countryRepository.GetPaginatedAsync(request.Page, request.PageSize, cancellationToken: cancellationToken);

                var dto = _mapper.Map<List<ProductionCountryDto>>(filtered);

                var total = await _countryRepository.GetCountAsync(cancellationToken: cancellationToken);

                var pageInfo = new PageInfo(request.Page, request.PageSize, total);

                return Result.Success(new PaginatedResponse<ProductionCountryDto>(dto, pageInfo));
            }
            catch (Exception ex)
            {
                return Result.Failure<PaginatedResponse<ProductionCountryDto>>(ProductionCountryApplicationErrors.FailedToGetCountries(ex.Message));
            }
        }
    }
}


