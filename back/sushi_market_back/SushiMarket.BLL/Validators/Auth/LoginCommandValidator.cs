using FluentValidation;
using SushiMarket.BLL.MediatR.Validators.Auth;

namespace SushiMarket.BLL.MediatR.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.loginRequest)
                .SetValidator(new UserLoginDtoValidator());
        }
    }
}