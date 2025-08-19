using FluentValidation;

namespace Application.Movies.AddGenreToMovie
{
    public class AddGenreToMovieCommandValidator : AbstractValidator<AddGenreToMovieCommand>
    {
        public AddGenreToMovieCommandValidator()
        {
            RuleFor(x => x.genreId)
                .NotEmpty().WithMessage("Genre ID cannot be empty.")
                .NotEqual(Guid.Empty).WithMessage("Genre ID cannot be an empty GUID.");

            RuleFor(x => x.movieId)
                .NotEmpty().WithMessage("Genre ID cannot be empty.")
                .NotEqual(Guid.Empty).WithMessage("Genre ID cannot be an empty GUID.");
        }
    }
}
