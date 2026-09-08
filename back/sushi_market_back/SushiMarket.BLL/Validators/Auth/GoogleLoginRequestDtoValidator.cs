using FluentValidation;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Validators.Auth
{
    public class GoogleLoginRequestDtoValidator : AbstractValidator<GoogleLoginRequestDto>
    {
        public GoogleLoginRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .ValidEmail(
                    maxLength: 255,
                    requiredMessage: ErrorMessages.EmailIsRequired,
                    formatMessage: ErrorMessages.InvalidEmailFormat,
                    lengthMessage: ErrorMessages.EmailMustNotExceedCharacters);

            RuleFor(x => x.Name)
                .RequiredWithMaxLength(
                    maxLength: 50,
                    requiredMessage: ErrorMessages.NameIsRequired,
                    lengthMessage: ErrorMessages.NameMustNotExceedCharacters);

            RuleFor(x => x.Surname)
                .RequiredWithMaxLength(
                    maxLength: 50,
                    requiredMessage: ErrorMessages.NameIsRequired,
                    lengthMessage: ErrorMessages.NameMustNotExceedCharacters);
        }
    }
}
