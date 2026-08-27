using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Categories.ReorderCategory;
using Xunit;

namespace SushiMarket.Tests.Validators.Categories
{
    public class ReorderCategoryCommandValidatorTests
    {
        private readonly ReorderCategoryCommandValidator _validator;

        public ReorderCategoryCommandValidatorTests()
        {
            _validator = new ReorderCategoryCommandValidator();
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(5, 10)]
        [InlineData(100, int.MaxValue)]
        public async Task Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors(int categoryId, double newSortOrder)
        {
            // Arrange
            var command = new ReorderCategoryCommand(categoryId, newSortOrder);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(-1, 5)]
        public async Task Validate_WhenCategoryIdIsInvalid_ShouldHaveValidationErrorForCategoryId(int categoryId, double newSortOrder)
        {
            // Arrange
            var command = new ReorderCategoryCommand(categoryId, newSortOrder);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }

        [Theory]
        [InlineData(1, -0.1)]
        [InlineData(1, -10)]
        public async Task Validate_WhenNewSortOrderIsNegative_ShouldHaveValidationErrorForNewSortOrder(int categoryId, double newSortOrder)
        {
            // Arrange
            var command = new ReorderCategoryCommand(categoryId, newSortOrder);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewSortOrder);
        }
    }
}