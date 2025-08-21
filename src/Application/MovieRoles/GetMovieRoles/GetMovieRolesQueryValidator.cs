using FluentValidation;

namespace Application.MovieRoles.GetMovieRoles
{
    public class GetMovieRolesQueryValidator : AbstractValidator<GetMovieRolesQuery>
    {
        public GetMovieRolesQueryValidator()
        {
            RuleFor(x => x.Page).InclusiveBetween(1, int.MaxValue)
                .WithMessage("Page must be a non-negative integer.");

            RuleFor(x => x.PageSize).InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");
        }
    }
}


