using FluentValidation;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Validators.Product;

namespace SushiMarket.BLL.Validators.Category
{
    public class CategoryWithProductsDtoValidator : AbstractValidator<CategoryWithProductsDto>
    {
        public CategoryWithProductsDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(ErrorMessages.CategoryIdRequired);

            RuleFor(x => x.TitleUa)
                .RequiredWithMaxLength(
                    maxLength: 100,
                    requiredMessage: ErrorMessages.CategoryTitleUaRequired,
                    lengthMessage: ErrorMessages.CategoryTitleUaMaxLength
                );

            RuleFor(x => x.TitleEn)
                .RequiredWithMaxLength(
                    maxLength: 100,
                    requiredMessage: ErrorMessages.CategoryTitleEnRequired,
                    lengthMessage: ErrorMessages.CategoryTitleEnMaxLength
                );

            RuleFor(x => x.ImgSrc)
                .NotEmpty().WithMessage(ErrorMessages.ImgSrcRequired);

            When(x => x.SortOrder.HasValue, () =>
            {
                RuleFor(x => x.SortOrder!.Value)
                    .GreaterThanOrEqualTo(0).WithMessage(ErrorMessages.SortOrderInvalid);
            });

            RuleForEach(x => x.Products)
                .SetValidator(new ProductDtoValidator());
        }
    }
}