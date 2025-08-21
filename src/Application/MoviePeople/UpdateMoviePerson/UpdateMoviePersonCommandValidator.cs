using FluentValidation;

namespace Application.MoviePeople.UpdateMoviePerson
{
    public class UpdateMoviePersonCommandValidator : AbstractValidator<UpdateMoviePersonCommand>
    {
        public UpdateMoviePersonCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        }
    }
}

