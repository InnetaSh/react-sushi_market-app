using FluentValidation;
using SushiMarket.BLL.MediatR.Validators.Auth;
using SushiMarket.BLL.Validators.Auth;

namespace SushiMarket.BLL.MediatR.Auth.Register
{
    public class RegisterCommandValidator
        : AbstractValidator<RegisterUserCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.registerRequest)
                .SetValidator(new UserRegisterDtoValidator());
        }
    }
}