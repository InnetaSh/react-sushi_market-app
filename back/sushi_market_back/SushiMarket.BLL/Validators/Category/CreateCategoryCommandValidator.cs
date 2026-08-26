using FluentValidation;
using SushiMarket.BLL.Resources;

namespace SushiMarket.BLL.MediatR.Categories.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.TitleUa) || !string.IsNullOrWhiteSpace(x.TitleEn))
                .WithMessage(ErrorMessages.AtLeastOneTitleRequired);

            RuleFor(x => x.TitleUa)
                .MaximumLength(100)
                .WithMessage(string.Format(ErrorMessages.MaxLengthExceeded, 100))
                .When(x => !string.IsNullOrWhiteSpace(x.TitleUa));

            RuleFor(x => x.TitleEn)
                .MaximumLength(100)
                .WithMessage(string.Format(ErrorMessages.MaxLengthExceeded, 100))
                .When(x => !string.IsNullOrWhiteSpace(x.TitleEn));
        }
    }
}