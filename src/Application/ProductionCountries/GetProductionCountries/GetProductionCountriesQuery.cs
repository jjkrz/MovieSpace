using Application.Abstractions;
using Application.Helpers;

namespace Application.ProductionCountries.GetProductionCountries
{
    public record GetProductionCountriesQuery(int Page, int PageSize) : IQuery<PaginatedResponse<ProductionCountryDto>>
    {
    }
}


