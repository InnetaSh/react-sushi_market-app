using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Products.CreateProduct;
using Xunit;

namespace SushiMarket.Tests.Validators.Products
{
    public class CreateProductCommandValidatorTests
    {
        private readonly CreateProductCommandValidator _validator;

        public CreateProductCommandValidatorTests()
        {
            _validator = new CreateProductCommandValidator();
        }

        [Fact]
        public async Task Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors()
        {
            // Arrange
            var command = new CreateProductCommand(
                CategoryId: 1,
                TitleUa: "Філадельфія",
                TitleEn: "Philadelphia",
                Price: 250.0m,
                WeightOrVolume: "250г",
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
        public async Task Validate_WhenCategoryIdIsInvalid_ShouldHaveValidationErrorForCategoryId(int categoryId)
        {
            // Arrange
            var command = new CreateProductCommand(
                CategoryId: categoryId,
                TitleUa: "Філадельфія",
                TitleEn: "Philadelphia",
                Price: 250.0m,
                WeightOrVolume: "250г",
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
            var command = new CreateProductCommand(
                CategoryId: 1,
                TitleUa: titleUa!,
                TitleEn: titleEn!,
                Price: 250.0m,
                WeightOrVolume: "250г",
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
            var command = new CreateProductCommand(
                CategoryId: 1,
                TitleUa: longTitle,
                TitleEn: null!,
                Price: 250.0m,
                WeightOrVolume: "250г",
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
            var command = new CreateProductCommand(
                CategoryId: 1,
                TitleUa: null!,
                TitleEn: longTitle,
                Price: 250.0m,
                WeightOrVolume: "250г",
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
        [InlineData(-10.5)]
        public async Task Validate_WhenPriceIsZeroOrNegative_ShouldHaveValidationErrorForPrice(decimal price)
        {
            // Arrange
            var command = new CreateProductCommand(
                CategoryId: 1,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: price,
                WeightOrVolume: "250г",
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
            var command = new CreateProductCommand(
                CategoryId: 1,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: 250.0m,
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
    }
}