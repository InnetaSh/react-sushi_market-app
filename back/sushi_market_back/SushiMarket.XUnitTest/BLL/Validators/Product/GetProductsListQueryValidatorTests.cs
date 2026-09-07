using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Products.GetProductsList;
using Xunit;

namespace SushiMarket.Tests.Validators.Products
{
    public class GetProductsListQueryValidatorTests
    {
        private readonly GetProductsListQueryValidator _validator;

        public GetProductsListQueryValidatorTests()
        {
            _validator = new GetProductsListQueryValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(int.MaxValue)]
        public async Task Validate_WhenCategoryIdIsNullOrPositive_ShouldNotHaveAnyValidationErrors(int? categoryId)
        {
            var query = new GetProductsListQuery(categoryId);

            var result = await _validator.TestValidateAsync(query);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Validate_WhenCategoryIdIsZeroOrNegative_ShouldHaveValidationErrorForCategoryId(int? categoryId)
        {
            var query = new GetProductsListQuery(categoryId);

            var result = await _validator.TestValidateAsync(query);

            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }
    }
}