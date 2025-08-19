using Application.Abstractions;

namespace Application.ProductionCountries.AddProductionCountry
{
    public record class AddProductionCountryCommand(string Name) : ICommand<AddProductionCountryResponse>
    {
    }
}


