using FluentValidation;
using SushiMarket.BLL.MediatR.Interface;
using SushiMarket.BLL.Resources;

namespace SushiMarket.BLL.Validators
{
    public class PositiveCategoryIdValidator<T> : AbstractValidator<T>
        where T : IHasCategoryId
    {
        public PositiveCategoryIdValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.IdMustBePositive);
        }
    }
}