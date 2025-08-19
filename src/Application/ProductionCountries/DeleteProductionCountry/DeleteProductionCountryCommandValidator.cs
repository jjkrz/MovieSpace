using FluentValidation;

namespace Application.ProductionCountries.DeleteProductionCountry
{
    public class DeleteProductionCountryCommandValidator : AbstractValidator<DeleteProductionCountryCommand>
    {
        public DeleteProductionCountryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Production country ID cannot be empty")
                .NotEqual(Guid.Empty).WithMessage("Production country ID cannot be an empty GUID.");
        }
    }
}


