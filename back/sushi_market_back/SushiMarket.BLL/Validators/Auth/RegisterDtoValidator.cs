using FluentValidation;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.Resources;

namespace SushiMarket.BLL.Validators.Auth
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
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

            RuleFor(x => x.Name)
                .RequiredWithMaxLength(
                    maxLength: 50,
                    requiredMessage: ErrorMessages.NameRequired,
                    lengthMessage: ErrorMessages.NameMaxLength
                );

            RuleFor(x => x.Surname)
                .RequiredWithMaxLength(
                    maxLength: 50,
                    requiredMessage: ErrorMessages.SurnameRequired,
                    lengthMessage: ErrorMessages.SurnameMaxLength
                );
        }
    }
}