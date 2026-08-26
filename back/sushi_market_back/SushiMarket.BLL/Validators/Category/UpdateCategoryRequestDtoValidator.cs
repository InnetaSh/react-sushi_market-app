using FluentValidation;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.Resources;

namespace SushiMarket.BLL.Validators.Category
{
    public class UpdateCategoryRequestDtoValidator : AbstractValidator<UpdateCategoryRequestDto>
    {
        public UpdateCategoryRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(ErrorMessages.CategoryIdRequired);

            When(x => !string.IsNullOrEmpty(x.TitleUa), () =>
            {
                RuleFor(x => x.TitleUa!)
                    .MaximumLength(100).WithMessage(ErrorMessages.CategoryTitleUaMaxLength);
            });

            When(x => !string.IsNullOrEmpty(x.TitleEn), () =>
            {
                RuleFor(x => x.TitleEn!)
                    .MaximumLength(100).WithMessage(ErrorMessages.CategoryTitleEnMaxLength);
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