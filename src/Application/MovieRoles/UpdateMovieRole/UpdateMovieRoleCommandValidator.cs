using FluentValidation;

namespace Application.MovieRoles.UpdateMovieRole
{
    public class UpdateMovieRoleCommandValidator : AbstractValidator<UpdateMovieRoleCommand>
    {
        public UpdateMovieRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotEqual(Guid.Empty);

            RuleFor(x => x.RoleName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}


