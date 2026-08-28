using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Categories.CreateCategory;
using Xunit;

namespace SushiMarket.Tests.Validators.Categories
{
    public class CreateCategoryCommandValidatorTests
    {
        private readonly CreateCategoryCommandValidator _validator;

        public CreateCategoryCommandValidatorTests()
        {
            _validator = new CreateCategoryCommandValidator();
        }

        [Fact]
        public async Task Validate_WhenAtLeastOneTitleProvided_ShouldNotHaveAnyValidationErrors()
        {
            // Arrange
            var commandWithUa = new CreateCategoryCommand("Роли", null!, "img.png", 1.0);
            var commandWithEn = new CreateCategoryCommand(null!, "Rolls", "img.png", 1.0);
            var commandWithBoth = new CreateCategoryCommand("Роли", "Rolls", "img.png", 1.0);

            // Act & Assert
            (await _validator.TestValidateAsync(commandWithUa)).ShouldNotHaveAnyValidationErrors();
            (await _validator.TestValidateAsync(commandWithEn)).ShouldNotHaveAnyValidationErrors();
            (await _validator.TestValidateAsync(commandWithBoth)).ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("   ", "   ")]
        public async Task Validate_WhenBothTitlesAreMissing_ShouldHaveValidationError(string? titleUa, string? titleEn)
        {
            // Arrange
            var command = new CreateCategoryCommand(titleUa!, titleEn!, "img.png", 1.0);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public async Task Validate_WhenTitleUaExceedsMaxLength_ShouldHaveValidationErrorForTitleUa()
        {
            // Arrange
            var longTitle = new string('a', 101); // > 100 chars
            var command = new CreateCategoryCommand(longTitle, null!, "img.png", 1.0);

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
            var command = new CreateCategoryCommand(null!, longTitle, "img.png", 1.0);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TitleEn);
        }
    }
}