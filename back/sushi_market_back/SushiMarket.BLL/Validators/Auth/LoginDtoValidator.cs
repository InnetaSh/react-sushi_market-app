using FluentValidation;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.Resources;

namespace SushiMarket.BLL.Validators.Auth
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .ValidEmail(
                    maxLength: 100,
                    requiredMessage: ErrorMessages.EmailRequired,
                    formatMessage: ErrorMessages.EmailInvalidFormat,
                    lengthMessage: ErrorMessages.EmailMaxLength
                );

            RuleFor(x => x.Password)
                .ValidPassword(
                    minLength: 6,
                    maxLength: 50,
                    requiredMessage: ErrorMessages.PasswordRequired,
                    minLengthMessage: ErrorMessages.PasswordMinLength,
                    maxLengthMessage: ErrorMessages.PasswordMaxLength
                );
        }
    }
}