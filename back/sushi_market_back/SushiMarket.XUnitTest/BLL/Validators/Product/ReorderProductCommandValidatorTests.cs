using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Products.ReorderProduct;
using Xunit;

namespace SushiMarket.Tests.Validators.Products
{
    public class ReorderProductCommandValidatorTests
    {
        private readonly ReorderProductCommandValidator _validator;

        public ReorderProductCommandValidatorTests()
        {
            _validator = new ReorderProductCommandValidator();
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(10, 5.5)]
        [InlineData(int.MaxValue, int.MaxValue)]
        public async Task Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors(int id, double newSortOrder)
        {
            // Arrange
            var command = new ReorderProductCommand(id, newSortOrder);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(-1, 1)]
        public async Task Validate_WhenIdIsInvalid_ShouldHaveValidationErrorForId(int id, double newSortOrder)
        {
            // Arrange
            var command = new ReorderProductCommand(id, newSortOrder);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData(1, -0.1)]
        [InlineData(1, -50)]
        public async Task Validate_WhenNewSortOrderIsNegative_ShouldHaveValidationErrorForNewSortOrder(int id, double newSortOrder)
        {
            // Arrange
            var command = new ReorderProductCommand(id, newSortOrder);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewSortOrder);
        }
    }
}