using Domain.Common;

namespace Application.ProductionCountries
{
    public static class ProductionCountryApplicationErrors
    {
        public static Error CountryAlreadyExists(string name) =>
            new Error(ErrorType.BadRequest, $"Production country with name '{name}' already exists.");

        public static Error CountryNotFound(Guid id) =>
            new Error(ErrorType.NotFound, $"The specified production country with id: {id} was not found.");

        public static Error FailedToGetCountries(string message) =>
            new Error(ErrorType.Unexpected, $"Failed to get production countries: {message}");
    }
}


