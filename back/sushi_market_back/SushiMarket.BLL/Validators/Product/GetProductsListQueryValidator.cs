using FluentValidation;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Products.GetProductsList
{
    public class GetProductsListQueryValidator : AbstractValidator<GetProductsListQuery>
    {
        public GetProductsListQueryValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.IdMustBePositive)
                .When(x => x.CategoryId.HasValue);
        }
    }
}