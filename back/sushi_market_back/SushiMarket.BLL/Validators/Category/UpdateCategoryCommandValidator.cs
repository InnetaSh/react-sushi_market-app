using FluentValidation;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Categories.UpdateCategory
{
    public class UpdateCategoryCommandValidator : PositiveIdValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.TitleUa)
                .MaximumLength(100)
                .WithMessage(string.Format(ErrorMessages.MaxLengthExceeded, 100))
                .When(x => !string.IsNullOrWhiteSpace(x.TitleUa));

            RuleFor(x => x.TitleEn)
                .MaximumLength(100)
                .WithMessage(string.Format(ErrorMessages.MaxLengthExceeded, 100))
                .When(x => !string.IsNullOrWhiteSpace(x.TitleEn));

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ErrorMessages.SortOrderMustBeNonNegative)
                .When(x => x.SortOrder.HasValue);
        }
    }
}