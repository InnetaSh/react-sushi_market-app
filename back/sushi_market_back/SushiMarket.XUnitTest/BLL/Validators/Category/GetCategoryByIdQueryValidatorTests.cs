using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Categories.GetCategoryById;
using Xunit;

namespace SushiMarket.Tests.Validators.Categories
{
    public class GetCategoryByIdQueryValidatorTests
    {
        private readonly GetCategoryByIdQueryValidator _validator;

        public GetCategoryByIdQueryValidatorTests()
        {
            _validator = new GetCategoryByIdQueryValidator();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(55)]
        [InlineData(int.MaxValue)]
        public async Task Validate_WhenIdIsPositive_ShouldNotHaveAnyValidationErrors(int id)
        {
            // Arrange
            var query = new GetCategoryByIdQuery(id);

            // Act
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-99)]
        public async Task Validate_WhenIdIsZeroOrNegative_ShouldHaveValidationErrorForId(int id)
        {
            // Arrange
            var query = new GetCategoryByIdQuery(id);

            // Act
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}