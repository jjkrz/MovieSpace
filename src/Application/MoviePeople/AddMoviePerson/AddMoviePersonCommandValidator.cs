using FluentValidation;

namespace Application.MoviePeople.AddMoviePerson
{
    public class AddMoviePersonCommandValidator : AbstractValidator<AddMoviePersonCommand>
    {
        public AddMoviePersonCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        }
    }
}

