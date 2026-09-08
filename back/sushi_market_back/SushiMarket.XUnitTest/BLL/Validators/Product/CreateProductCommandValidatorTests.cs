using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;
using SushiMarket.BLL.MediatR.Products.CreateProduct;
using Xunit;

namespace SushiMarket.Tests.Validators.Products
{
    public class CreateProductCommandValidatorTests
    {
        private readonly CreateProductCommandValidator _validator;
        private readonly Mock<IFormFile> _fileMock;

        public CreateProductCommandValidatorTests()
        {
            _validator = new CreateProductCommandValidator();
            _fileMock = new Mock<IFormFile>();
        }

        [Fact]
        public async Task Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors()
        {
            var command = new CreateProductCommand(
                TitleUa: "Філадельфія",
                TitleEn: "Philadelphia",
                DescriptionUa: "Опис",
                DescriptionEn: "Description",
                WeightOrVolume: "250г",
                Price: 250.0m,
                Image: _fileMock.Object,
                SortOrder: 1.0,
                CategoryId: 1
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Validate_WhenCategoryIdIsInvalid_ShouldHaveValidationErrorForCategoryId(int categoryId)
        {
            var command = new CreateProductCommand(
                TitleUa: "Філадельфія",
                TitleEn: "Philadelphia",
                DescriptionUa: null!,
                DescriptionEn: null!,
                WeightOrVolume: "250г",
                Price: 250.0m,
                Image: _fileMock.Object,
                SortOrder: null,
                CategoryId: categoryId
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("    ", "    ")]
        public async Task Validate_WhenBothTitlesAreMissing_ShouldHaveValidationError(string? titleUa, string? titleEn)
        {
            var command = new CreateProductCommand(
                TitleUa: titleUa!,
                TitleEn: titleEn!,
                DescriptionUa: null!,
                DescriptionEn: null!,
                WeightOrVolume: "250г",
                Price: 250.0m,
                Image: _fileMock.Object,
                SortOrder: null,
                CategoryId: 1
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public async Task Validate_WhenTitleUaExceedsMaxLength_ShouldHaveValidationErrorForTitleUa()
        {
            var longTitle = new string('a', 101);
            var command = new CreateProductCommand(
                TitleUa: longTitle,
                TitleEn: null!,
                DescriptionUa: null!,
                DescriptionEn: null!,
                WeightOrVolume: "250г",
                Price: 250.0m,
                Image: _fileMock.Object,
                SortOrder: null,
                CategoryId: 1
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.TitleUa);
        }

        [Fact]
        public async Task Validate_WhenTitleEnExceedsMaxLength_ShouldHaveValidationErrorForTitleEn()
        {
            var longTitle = new string('a', 101);
            var command = new CreateProductCommand(
                TitleUa: null!,
                TitleEn: longTitle,
                DescriptionUa: null!,
                DescriptionEn: null!,
                WeightOrVolume: "250г",
                Price: 250.0m,
                Image: _fileMock.Object,
                SortOrder: null,
                CategoryId: 1
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.TitleEn);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10.5)]
        public async Task Validate_WhenPriceIsZeroOrNegative_ShouldHaveValidationErrorForPrice(decimal price)
        {
            var command = new CreateProductCommand(
                TitleUa: "Філадельфія",
                TitleEn: null!,
                DescriptionUa: null!,
                DescriptionEn: null!,
                WeightOrVolume: "250г",
                Price: price,
                Image: _fileMock.Object,
                SortOrder: null,
                CategoryId: 1
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        public async Task Validate_WhenWeightOrVolumeIsEmpty_ShouldHaveValidationErrorForWeightOrVolume(string? weight)
        {
            var command = new CreateProductCommand(
                TitleUa: "Філадельфія",
                TitleEn: null!,
                DescriptionUa: null!,
                DescriptionEn: null!,
                WeightOrVolume: weight!,
                Price: 250.0m,
                Image: _fileMock.Object,
                SortOrder: null,
                CategoryId: 1
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.WeightOrVolume);
        }
    }
}