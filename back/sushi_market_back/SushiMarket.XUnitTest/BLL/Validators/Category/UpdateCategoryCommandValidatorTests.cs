using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Categories.UpdateCategory;
using Xunit;

namespace SushiMarket.Tests.Validators.Categories
{
    public class UpdateCategoryCommandValidatorTests
    {
        private readonly UpdateCategoryCommandValidator _validator;

        public UpdateCategoryCommandValidatorTests()
        {
            _validator = new UpdateCategoryCommandValidator();
        }

        [Fact]
        public async Task Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors()
        {
            // Arrange
            var command = new UpdateCategoryCommand(1, "Нові роли", "New Rolls", 1.0, "img.png");

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
            var command = new UpdateCategoryCommand(id, "Роли", "Rolls", 1.0, "img.png");

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public async Task Validate_WhenTitleUaExceedsMaxLength_ShouldHaveValidationErrorForTitleUa()
        {
            // Arrange
            var longTitle = new string('a', 101); // > 100 chars
            var command = new UpdateCategoryCommand(1, longTitle, "Rolls", 1.0, "img.png");

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TitleUa);
        }

        [Fact]
        public async Task Validate_WhenTitleEnExceedsMaxLength_ShouldHaveValidationErrorForTitleEn()
        {
            // Arrange
            var longTitle = new string('a', 101); // > 100 chars
            var command = new UpdateCategoryCommand(1, "Роли", longTitle, 1.0, "img.png");

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TitleEn);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-5)]
        public async Task Validate_WhenSortOrderIsNegative_ShouldHaveValidationErrorForSortOrder(double sortOrder)
        {
            // Arrange
            var command = new UpdateCategoryCommand(1, "Роли", "Rolls", sortOrder, "img.png");

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SortOrder);
        }
    }
}