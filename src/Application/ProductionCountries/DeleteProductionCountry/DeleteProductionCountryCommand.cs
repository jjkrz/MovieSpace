using Application.Abstractions;

namespace Application.ProductionCountries.DeleteProductionCountry
{
    public record DeleteProductionCountryCommand(Guid Id) : ICommand
    {
    }
}


