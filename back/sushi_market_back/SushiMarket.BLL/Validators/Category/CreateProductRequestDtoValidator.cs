using FluentValidation;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.Resources;
using Streetcode.Auth.Validators;

namespace SushiMarket.BLL.Validators.Product
{
    public class CreateProductRequestDtoValidator : AbstractValidator<CreateProductRequestDto>
    {
        public CreateProductRequestDtoValidator()
        {
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

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage(ErrorMessages.CategoryIdRequired);

            RuleFor(x => x.Image)
                .NotNull().WithMessage(ErrorMessages.ImgSrcRequired);

            When(x => x.SortOrder.HasValue, () =>
            {
                RuleFor(x => x.SortOrder!.Value)
                    .GreaterThanOrEqualTo(0).WithMessage(ErrorMessages.SortOrderInvalid);
            });
        }
    }
}