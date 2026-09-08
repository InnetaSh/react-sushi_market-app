using FluentValidation;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.Resources;

namespace SushiMarket.BLL.MediatR.Validators.Auth
{
    public class UserLoginDtoValidator : BaseUserValidator<UserLoginDto>
    {
        private const int MaxLoginLength = 20;
        private const int MaxPasswordLength = 20;
        private const int MinPasswordLength = 8;

        public UserLoginDtoValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            ApplyLoginRules(x => x.Login, MaxLoginLength);

            ApplyPasswordRules(x => x.Password, MinPasswordLength, MaxPasswordLength,
                 ErrorMessages.PasswordIsRequired,
                 ErrorMessages.PasswordMustBeAtLeastCharacters,
                 ErrorMessages.PasswordMustNotExceedCharacters);
        }
    }
}