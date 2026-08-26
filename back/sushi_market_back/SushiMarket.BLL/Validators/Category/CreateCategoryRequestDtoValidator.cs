using FluentValidation;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.Resources;

namespace SushiMarket.BLL.Validators.Category
{
    public class CreateCategoryRequestDtoValidator : AbstractValidator<CreateCategoryRequestDto>
    {
        public CreateCategoryRequestDtoValidator()
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