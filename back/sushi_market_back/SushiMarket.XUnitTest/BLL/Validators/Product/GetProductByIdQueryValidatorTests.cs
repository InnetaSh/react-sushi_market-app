using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Products.GetProductById;
using Xunit;

namespace SushiMarket.Tests.Validators.Products
{
    public class GetProductByIdQueryValidatorTests
    {
        private readonly GetProductByIdQueryValidator _validator;

        public GetProductByIdQueryValidatorTests()
        {
            _validator = new GetProductByIdQueryValidator();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(77)]
        [InlineData(int.MaxValue)]
        public async Task Validate_WhenIdIsPositive_ShouldNotHaveAnyValidationErrors(int id)
        {
            // Arrange
            var query = new GetProductByIdQuery(id);

            // Act
            var result = await _validator.TestValidateAsync(query);

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
            var query = new GetProductByIdQuery(id);

            // Act
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}