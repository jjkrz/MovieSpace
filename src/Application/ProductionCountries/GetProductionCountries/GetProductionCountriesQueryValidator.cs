using FluentValidation;

namespace Application.ProductionCountries.GetProductionCountries
{
    public class GetProductionCountriesQueryValidator : AbstractValidator<GetProductionCountriesQuery>
    {
        public GetProductionCountriesQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1).WithMessage("Page must be at least 1.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(1000).WithMessage("Page size cannot exceed 1000.");
        }
    }
}


