using FluentValidation;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.Resources;

namespace SushiMarket.BLL.Validators.Product
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
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

            RuleFor(x => x.DescriptionUa)
                .MaximumLength(500).WithMessage(ErrorMessages.DescriptionUaMaxLength);

            RuleFor(x => x.DescriptionEn)
                .MaximumLength(500).WithMessage(ErrorMessages.DescriptionEnMaxLength);

            RuleFor(x => x.WeightOrVolume)
                .NotEmpty().WithMessage(ErrorMessages.WeightOrVolumeRequired)
                .MaximumLength(50).WithMessage(ErrorMessages.WeightOrVolumeMaxLength);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage(ErrorMessages.PriceGreaterThanZero);

            RuleFor(x => x.ImgSrc)
                .NotEmpty().WithMessage(ErrorMessages.ImgSrcRequired);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage(ErrorMessages.CategoryIdRequired);

            When(x => x.SortOrder.HasValue, () =>
            {
                RuleFor(x => x.SortOrder!.Value)
                    .GreaterThanOrEqualTo(0).WithMessage(ErrorMessages.SortOrderInvalid);
            });

            When(x => x.LikesCount.HasValue, () =>
            {
                RuleFor(x => x.LikesCount!.Value)
                    .GreaterThanOrEqualTo(0).WithMessage(ErrorMessages.LikesCountInvalid);
            });
        }
    }
}