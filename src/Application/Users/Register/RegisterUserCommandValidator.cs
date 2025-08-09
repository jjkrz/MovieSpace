using FluentValidation;

namespace Application.Users.Register
{
    public class RegisterUserCommandValidator: AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator() // only check if the fields are not empty, identity will handle the rest
        {
            RuleFor(x => x.UserName).NotEmpty();

            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
