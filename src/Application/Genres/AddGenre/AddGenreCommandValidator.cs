using FluentValidation;

namespace Application.Genres.AddGenre
{
    public class AddGenreCommandValidator: AbstractValidator<AddGenreCommand>
    {
        public AddGenreCommandValidator()
        {
            RuleFor(x => x.GenreName).NotEmpty()
                .WithMessage("Genre name cannot be empty.")
                .MaximumLength(100)
                .WithMessage("Genre name cannot exceed 100 characters.");
        }
    }
}
