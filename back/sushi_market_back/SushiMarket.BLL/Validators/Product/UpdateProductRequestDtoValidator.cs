using FluentValidation;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.Resources;

namespace SushiMarket.BLL.Validators.Product
{
    public class UpdateProductRequestDtoValidator : AbstractValidator<UpdateProductRequestDto>
    {
        public UpdateProductRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(ErrorMessages.CategoryIdRequired);

            When(x => !string.IsNullOrEmpty(x.TitleUa), () =>
            {
                RuleFor(x => x.TitleUa)
                    .MaximumLength(100).WithMessage(ErrorMessages.CategoryTitleUaMaxLength);
            });

            When(x => !string.IsNullOrEmpty(x.TitleEn), () =>
            {
                RuleFor(x => x.TitleEn)
                    .MaximumLength(100).WithMessage(ErrorMessages.CategoryTitleEnMaxLength);
            });

            When(x => !string.IsNullOrEmpty(x.DescriptionUa), () =>
            {
                RuleFor(x => x.DescriptionUa)
                    .MaximumLength(500).WithMessage(ErrorMessages.DescriptionUaMaxLength);
            });

            When(x => !string.IsNullOrEmpty(x.DescriptionEn), () =>
            {
                RuleFor(x => x.DescriptionEn)
                    .MaximumLength(500).WithMessage(ErrorMessages.DescriptionEnMaxLength);
            });

            When(x => !string.IsNullOrEmpty(x.WeightOrVolume), () =>
            {
                RuleFor(x => x.WeightOrVolume)
                    .MaximumLength(50).WithMessage(ErrorMessages.WeightOrVolumeMaxLength);
            });

            When(x => x.Price > 0, () =>
            {
                RuleFor(x => x.Price)
                    .GreaterThan(0).WithMessage(ErrorMessages.PriceGreaterThanZero);
            });

            When(x => x.CategoryId > 0, () =>
            {
                RuleFor(x => x.CategoryId)
                    .GreaterThan(0).WithMessage(ErrorMessages.CategoryIdRequired);
            });

            When(x => x.SortOrder.HasValue, () =>
            {
                RuleFor(x => x.SortOrder!.Value)
                    .GreaterThanOrEqualTo(0).WithMessage(ErrorMessages.SortOrderInvalid);
            });

            When(x => x.Image != null, () =>
            {
                RuleFor(x => x.Image!)
                    .NotNull().WithMessage(ErrorMessages.ImgSrcRequired);
            });
        }
    }
}