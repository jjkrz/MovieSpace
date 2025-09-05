using FluentValidation;

namespace Application.Movies.GetMovieDetails
{
    public class GetMovieDetailsQueryValidator : AbstractValidator<GetMovieDetailsQuery>
    {
        public GetMovieDetailsQueryValidator()
        {
            RuleFor(x => x.MovieId)
                .NotEmpty()
                .WithMessage("Movie Id cannot be empty");
        }
    }
}
