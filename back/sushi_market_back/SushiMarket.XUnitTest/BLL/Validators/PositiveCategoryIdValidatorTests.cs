using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Interface;
using SushiMarket.BLL.Validators;
using Xunit;

namespace SushiMarket.Tests.Validators
{
    public class PositiveCategoryIdValidatorTests
    {
        private class TestCategoryRequest : IHasCategoryId
        {
            public int CategoryId { get; set; }
        }

        private readonly PositiveCategoryIdValidator<TestCategoryRequest> _validator;

        public PositiveCategoryIdValidatorTests()
        {
            _validator = new PositiveCategoryIdValidator<TestCategoryRequest>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(42)]
        [InlineData(int.MaxValue)]
        public async Task Validate_WhenCategoryIdIsPositive_ShouldNotHaveAnyValidationErrors(int categoryId)
        {
            // Arrange
            var model = new TestCategoryRequest { CategoryId = categoryId };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public async Task Validate_WhenCategoryIdIsZeroOrNegative_ShouldHaveValidationErrorForCategoryId(int categoryId)
        {
            // Arrange
            var model = new TestCategoryRequest { CategoryId = categoryId };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }
    }
}