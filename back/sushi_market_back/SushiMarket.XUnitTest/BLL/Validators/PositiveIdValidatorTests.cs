using FluentValidation.TestHelper;
using SushiMarket.BLL.MediatR.Interface;
using SushiMarket.BLL.Validators;
using Xunit;

namespace SushiMarket.Tests.Validators
{
    public class PositiveIdValidatorTests
    {
        private class TestRequest : IHasId
        {
            public int Id { get; set; }
        }

        private readonly PositiveIdValidator<TestRequest> _validator;

        public PositiveIdValidatorTests()
        {
            _validator = new PositiveIdValidator<TestRequest>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        public async Task Validate_WhenIdIsPositive_ShouldNotHaveAnyValidationErrors(int id)
        {
            // Arrange
            var model = new TestRequest { Id = id };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-15)]
        public async Task Validate_WhenIdIsZeroOrNegative_ShouldHaveValidationErrorForId(int id)
        {
            // Arrange
            var model = new TestRequest { Id = id };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}