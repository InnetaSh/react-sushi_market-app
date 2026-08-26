using FluentValidation;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Products.UpdateProduct
{
    public class UpdateProductCommandValidator : PositiveIdValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.IdMustBePositive);

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

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.PriceMustBePositive);

            RuleFor(x => x.WeightOrVolume)
                .NotEmpty()
                .WithMessage(string.Format(ErrorMessages.FieldIsRequired, nameof(UpdateProductCommand.WeightOrVolume)));

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ErrorMessages.SortOrderMustBeNonNegative)
                .When(x => x.SortOrder.HasValue);
        }
    }
}