using FluentValidation;

namespace Application.Genres.GetGenres
{
    public class GetGenresQueryValidator : AbstractValidator<GetGenresQuery>
    {
        public GetGenresQueryValidator()
        {
            RuleFor(x => x.Page).InclusiveBetween(0, int.MaxValue)
                .WithMessage("Page must be a non-negative integer.");

            RuleFor(x => x.PageSize).InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
