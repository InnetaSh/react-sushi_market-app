using FluentValidation;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Validators.Auth
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        private const int MaxNameLength = 50;
        private const int MaxLoginLength = 20;

        public UserDtoValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .RequiredWithMaxLength(MaxNameLength, ErrorMessages.NameIsRequired, ErrorMessages.NameMustNotExceedCharacters);

            RuleFor(x => x.Surname)
                .RequiredWithMaxLength(MaxNameLength, ErrorMessages.SurnameIsRequired, ErrorMessages.SurnameMustNotExceedCharacters);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ErrorMessages.EmailIsRequired)
                .EmailAddress().WithMessage(ErrorMessages.InvalidEmailFormat);

            RuleFor(x => x.Login)
                .RequiredWithMaxLength(MaxLoginLength, ErrorMessages.LoginIsRequired, ErrorMessages.LoginMustNotExceedCharacters);

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage(ErrorMessages.InvalidUserRole);
        }
    }
}