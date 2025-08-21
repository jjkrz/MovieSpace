using FluentValidation;

namespace Application.MovieRoles.AddMovieRole
{
    public class AddMovieRoleCommandValidator : AbstractValidator<AddMovieRoleCommand>
    {
        public AddMovieRoleCommandValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}


