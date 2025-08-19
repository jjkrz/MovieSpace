using FluentValidation;

namespace Application.Genres.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand> 
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Genre ID cannot be empty")
                .NotEqual(Guid.Empty).WithMessage("Genre ID cannot be an empty GUID.");
        }
    }
}
