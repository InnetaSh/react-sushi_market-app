using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Products.DeleteProduct;
using Xunit;

namespace SushiMarket.Tests.Validators.Products
{
    public class DeleteProductCommandValidatorTests
    {
        private readonly DeleteProductCommandValidator _validator;

        public DeleteProductCommandValidatorTests()
        {
            _validator = new DeleteProductCommandValidator();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(150)]
        [InlineData(int.MaxValue)]
        public async Task Validate_WhenIdIsPositive_ShouldNotHaveAnyValidationErrors(int id)
        {
            // Arrange
            var command = new DeleteProductCommand(id);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-42)]
        public async Task Validate_WhenIdIsZeroOrNegative_ShouldHaveValidationErrorForId(int id)
        {
            // Arrange
            var command = new DeleteProductCommand(id);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}