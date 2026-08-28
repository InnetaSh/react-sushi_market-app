using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Categories.DeleteCategory;
using Xunit;

namespace SushiMarket.Tests.Validators.Categories
{
    public class DeleteCategoryCommandValidatorTests
    {
        private readonly DeleteCategoryCommandValidator _validator;

        public DeleteCategoryCommandValidatorTests()
        {
            _validator = new DeleteCategoryCommandValidator();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public async Task Validate_WhenIdIsPositive_ShouldNotHaveAnyValidationErrors(int id)
        {
            // Arrange
            var command = new DeleteCategoryCommand(id);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task Validate_WhenIdIsZeroOrNegative_ShouldHaveValidationErrorForId(int id)
        {
            // Arrange
            var command = new DeleteCategoryCommand(id);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}