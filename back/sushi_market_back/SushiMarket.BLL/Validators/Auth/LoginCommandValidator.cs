using FluentValidation;
using SushiMarket.BLL.Validators.Auth;

namespace SushiMarket.BLL.MediatR.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Model)
                .SetValidator(new LoginDtoValidator());
        }
    }
}