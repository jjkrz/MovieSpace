using FluentValidation;

namespace Application.MoviePeople.DeleteMoviePerson
{
    public class DeleteMoviePersonCommandValidator : AbstractValidator<DeleteMoviePersonCommand>
    {
        public DeleteMoviePersonCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Movie person ID cannot be empty")
                .NotEqual(Guid.Empty).WithMessage("Movie person ID cannot be an empty GUID.");
        }
    }
}

