using FluentValidation;
using SushiMarket.BLL.Validators.Auth;

namespace SushiMarket.BLL.MediatR.Auth.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Model)
                .SetValidator(new RegisterDtoValidator());
        }
    }
}