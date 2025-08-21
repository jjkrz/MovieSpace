using FluentValidation;

namespace Application.MovieRoles.DeleteMovieRole
{
    public class DeleteMovieRoleCommandValidator : AbstractValidator<DeleteMovieRoleCommand>
    {
        public DeleteMovieRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotEqual(Guid.Empty);
        }
    }
}


