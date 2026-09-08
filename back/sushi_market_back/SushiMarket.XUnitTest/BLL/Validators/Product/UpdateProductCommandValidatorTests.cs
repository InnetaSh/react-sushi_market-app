using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;
using SushiMarket.BLL.MediatR.Products.UpdateProduct;
using Xunit;

namespace SushiMarket.Tests.Validators.Products
{
    public class UpdateProductCommandValidatorTests
    {
        private readonly UpdateProductCommandValidator _validator;
        private readonly Mock<IFormFile> _fileMock;

        public UpdateProductCommandValidatorTests()
        {
            _validator = new UpdateProductCommandValidator();
            _fileMock = new Mock<IFormFile>();
        }

        [Fact]
        public async Task Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors()
        {
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: "Філадельфія оновлена",
                TitleEn: "Updated Philadelphia",
                Price: 280.0m,
                WeightOrVolume: "260г",
                Image: _fileMock.Object,
                DescriptionUa: "Опис",
                DescriptionEn: "Description",
                SortOrder: 1.0
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Validate_WhenIdIsInvalid_ShouldHaveValidationErrorForId(int id)
        {
            var command = new UpdateProductCommand(
                Id: id,
                CategoryId: 2,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: 280.0m,
                WeightOrVolume: "260г",
                Image: _fileMock.Object,
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Validate_WhenCategoryIdIsInvalid_ShouldHaveValidationErrorForCategoryId(int categoryId)
        {
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: categoryId,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: 280.0m,
                WeightOrVolume: "260г",
                Image: _fileMock.Object,
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
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
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: titleUa!,
                TitleEn: titleEn!,
                Price: 280.0m,
                WeightOrVolume: "260г",
                Image: _fileMock.Object,
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public async Task Validate_WhenTitleUaExceedsMaxLength_ShouldHaveValidationErrorForTitleUa()
        {
            var longTitle = new string('a', 101);
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: longTitle,
                TitleEn: null!,
                Price: 280.0m,
                WeightOrVolume: "260г",
                Image: _fileMock.Object,
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.TitleUa);
        }

        [Fact]
        public async Task Validate_WhenTitleEnExceedsMaxLength_ShouldHaveValidationErrorForTitleEn()
        {
            var longTitle = new string('a', 101);
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: null!,
                TitleEn: longTitle,
                Price: 280.0m,
                WeightOrVolume: "260г",
                Image: _fileMock.Object,
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.TitleEn);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10.0)]
        public async Task Validate_WhenPriceIsZeroOrNegative_ShouldHaveValidationErrorForPrice(decimal price)
        {
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: price,
                WeightOrVolume: "260г",
                Image: _fileMock.Object,
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
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
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: 280.0m,
                WeightOrVolume: weight!,
                Image: _fileMock.Object,
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: null
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.WeightOrVolume);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-5)]
        public async Task Validate_WhenSortOrderIsNegative_ShouldHaveValidationErrorForSortOrder(double sortOrder)
        {
            var command = new UpdateProductCommand(
                Id: 1,
                CategoryId: 2,
                TitleUa: "Філадельфія",
                TitleEn: null!,
                Price: 280.0m,
                WeightOrVolume: "260г",
                Image: _fileMock.Object,
                DescriptionUa: null!,
                DescriptionEn: null!,
                SortOrder: sortOrder
            );

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.SortOrder);
        }
    }
}