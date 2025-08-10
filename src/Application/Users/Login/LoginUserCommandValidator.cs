using FluentValidation;

namespace Application.Users.Login
{
    public class LoginUserCommandValidator: AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
