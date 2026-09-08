using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Categories.GetCategoryWithProducts;
using Xunit;

namespace SushiMarket.Tests.Validators.Categories
{
    public class GetCategoryWithProductsQueryValidatorTests
    {
        private readonly GetCategoryWithProductsQueryValidator _validator;

        public GetCategoryWithProductsQueryValidatorTests()
        {
            _validator = new GetCategoryWithProductsQueryValidator();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        public async Task Validate_WhenCategoryIdIsPositive_ShouldNotHaveAnyValidationErrors(int categoryId)
        {
            var query = new GetCategoryWithProductsQuery(categoryId);

            var result = await _validator.TestValidateAsync(query);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Validate_WhenCategoryIdIsZeroOrNegative_ShouldHaveValidationErrorForCategoryId(int categoryId)
        {
            var query = new GetCategoryWithProductsQuery(categoryId);

            var result = await _validator.TestValidateAsync(query);

            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }
    }
}