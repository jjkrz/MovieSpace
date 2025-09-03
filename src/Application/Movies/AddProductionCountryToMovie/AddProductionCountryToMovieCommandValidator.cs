using FluentValidation;

namespace Application.Movies.AddProductionCountryToMovie
{
    public class AddProductionCountryToMovieCommandValidator : AbstractValidator<AddProductionCountryToMovieCommand>
    {
        public AddProductionCountryToMovieCommandValidator()
        {
            RuleFor(x => x.MovieId).NotEmpty();
            RuleFor(x => x.ProductionCountryId).NotEmpty();
        }
    }
}
