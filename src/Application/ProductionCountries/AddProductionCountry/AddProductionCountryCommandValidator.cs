using FluentValidation;

namespace Application.ProductionCountries.AddProductionCountry
{
    public class AddProductionCountryCommandValidator : AbstractValidator<AddProductionCountryCommand>
    {
        public AddProductionCountryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Production country name cannot be empty.")
                .MaximumLength(100).WithMessage("Production country name cannot exceed 100 characters.");
        }
    }
}


