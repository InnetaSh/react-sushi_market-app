using FluentValidation;
using SushiMarket.BLL.MediatR.Interface;
using SushiMarket.BLL.Resources;

namespace SushiMarket.BLL.Validators
{
    public class PositiveIdValidator<T> : AbstractValidator<T>
        where T : IHasId
    {
        public PositiveIdValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.IdMustBePositive);
        }
    }
}