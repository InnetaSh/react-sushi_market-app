using FluentValidation;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Categories.ReorderCategory
{
    public class ReorderCategoryCommandValidator : PositiveCategoryIdValidator<ReorderCategoryCommand>
    {
        public ReorderCategoryCommandValidator()
        {
            RuleFor(x => x.NewSortOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ErrorMessages.SortOrderMustBeNonNegative);
        }
    }
}