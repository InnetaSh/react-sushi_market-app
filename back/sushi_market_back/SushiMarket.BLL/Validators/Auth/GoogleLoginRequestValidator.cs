using FluentValidation;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.Resources;


namespace SushiMarket.BLL.MediatR.Validators.Auth
{
    public class GoogleLoginRequestValidator : AbstractValidator<GoogleLoginRequest>
    {
        public GoogleLoginRequestValidator()
        {
            RuleFor(x => x.IdToken)
                .NotEmpty().WithMessage(ErrorMessages.GoogleIDTokenIsRequired)
                .MinimumLength(100).WithMessage(ErrorMessages.InvalidTokenFormat);
        }
    }
}
