using FluentValidation;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Products.ReorderProduct
{
    public class ReorderProductCommandValidator : PositiveIdValidator<ReorderProductCommand>
    {
        public ReorderProductCommandValidator()
        {
            RuleFor(x => x.NewSortOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ErrorMessages.SortOrderMustBeNonNegative);
        }
    }
}