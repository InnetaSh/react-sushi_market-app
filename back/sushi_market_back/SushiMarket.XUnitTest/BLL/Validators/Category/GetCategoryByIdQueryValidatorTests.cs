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
            var query = new GetCategoryByIdQuery(id);

            var result = await _validator.TestValidateAsync(query);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-99)]
        public async Task Validate_WhenIdIsZeroOrNegative_ShouldHaveValidationErrorForId(int id)
        {
            var query = new GetCategoryByIdQuery(id);

            var result = await _validator.TestValidateAsync(query);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}