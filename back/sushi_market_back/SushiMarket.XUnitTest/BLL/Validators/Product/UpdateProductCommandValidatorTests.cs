using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Products.UpdateProduct;
using Xunit;

namespace SushiMarket.Tests.Validators.Products
{
    public class UpdateProductCommandValidatorTests
    {
        private readonly UpdateProductCommandValidator _validator;

        public UpdateProductCommandValidatorTests()
        {
            _validator = new UpdateProductCommandValidator();
        }

        [Fact]
        public async Task Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors()
        {
            // Arrange
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: "Філадельфія оновлена",
                TitleEn: "Updated Philadelphia",
                Price: 280.0m,
                WeightOrVolume: "260г",
                ImgSrc: "img.png",
                DescriptionUa: "Опис",
                DescriptionEn: "Description",
                SortOrder: 1.0
            );

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Validate_WhenIdIsInvalid_ShouldHaveValidationErrorForId(int id)
        {
            // Arrange
            var command = new UpdateProductCommand(
                Id: id,
                CategoryId: 2,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: 280.0m,
                WeightOrVolume: "260г",
                ImgSrc: "img.png",
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Validate_WhenCategoryIdIsInvalid_ShouldHaveValidationErrorForCategoryId(int categoryId)
        {
            // Arrange
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: categoryId,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: 280.0m,
                WeightOrVolume: "260г",
                ImgSrc: "img.png",
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("   ", "   ")]
        public async Task Validate_WhenBothTitlesAreMissing_ShouldHaveValidationError(string? titleUa, string? titleEn)
        {
            // Arrange
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: titleUa!,
                TitleEn: titleEn!,
                Price: 280.0m,
                WeightOrVolume: "260г",
                ImgSrc: "img.png",
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public async Task Validate_WhenTitleUaExceedsMaxLength_ShouldHaveValidationErrorForTitleUa()
        {
            // Arrange
            var longTitle = new string('a', 101);
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: longTitle,
                TitleEn: null!,
                Price: 280.0m,
                WeightOrVolume: "260г",
                ImgSrc: "img.png",
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TitleUa);
        }

        [Fact]
        public async Task Validate_WhenTitleEnExceedsMaxLength_ShouldHaveValidationErrorForTitleEn()
        {
            // Arrange
            var longTitle = new string('a', 101);
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: null!,
                TitleEn: longTitle,
                Price: 280.0m,
                WeightOrVolume: "260г",
                ImgSrc: "img.png",
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TitleEn);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10.0)]
        public async Task Validate_WhenPriceIsZeroOrNegative_ShouldHaveValidationErrorForPrice(decimal price)
        {
            // Arrange
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: price,
                WeightOrVolume: "260г",
                ImgSrc: "img.png",
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task Validate_WhenWeightOrVolumeIsEmpty_ShouldHaveValidationErrorForWeightOrVolume(string? weight)
        {
            // Arrange
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: 280.0m,
                WeightOrVolume: weight!,
                ImgSrc: "img.png",
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.WeightOrVolume);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-5)]
        public async Task Validate_WhenSortOrderIsNegative_ShouldHaveValidationErrorForSortOrder(double sortOrder)
        {
            // Arrange
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: 280.0m,
                WeightOrVolume: "260г",
                ImgSrc: "img.png",
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: sortOrder
            );

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SortOrder);
        }
    }
}