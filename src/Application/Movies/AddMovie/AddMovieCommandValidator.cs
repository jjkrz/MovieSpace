using FluentValidation;

namespace Application.Movies.AddMovie
{
    public class AddMovieCommandValidator: AbstractValidator<AddMovieCommand>
    {
        public AddMovieCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1200).WithMessage("Description must not exceed 1200 characters.");

            RuleFor(x => x.Duration)
                .NotEmpty().WithMessage("Duration is required.")
                .Must(duration => !duration.Equals(0)).WithMessage("Duration must be greater than zero.");

            RuleFor(x => x.ReleaseDate)
                .NotEmpty().WithMessage("Release date is required.");

            RuleFor(x => x.PosterUri)
                .Must(uri => uri == null || Uri.IsWellFormedUriString(uri.ToString(), UriKind.Absolute))
                .WithMessage("Poster URI must be a valid absolute URI.");

        }
    }
}
