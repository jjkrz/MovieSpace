using FluentValidation;

namespace Application.Movies.RateMovie
{
    public class RateMovieCommandValidator: AbstractValidator<RateMovieCommand>
    {
        public RateMovieCommandValidator()
        {
            RuleFor(x => x.Score)
                .InclusiveBetween(1, 10)
                .WithMessage("Rating score must be between 1 and 10");

            RuleFor(x => x.MovieId)
                .NotEmpty()
                .WithMessage("MovieId must be provided");
        }
    }
}
