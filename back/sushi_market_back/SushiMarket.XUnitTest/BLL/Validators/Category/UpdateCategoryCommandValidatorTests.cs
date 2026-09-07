using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;
using SushiMarket.BLL.MediatR.Categories.UpdateCategory;
using Xunit;

namespace SushiMarket.Tests.Validators.Categories
{
    public class UpdateCategoryCommandValidatorTests
    {
        private readonly UpdateCategoryCommandValidator _validator;
        private readonly Mock<IFormFile> _fileMock;

        public UpdateCategoryCommandValidatorTests()
        {
            _validator = new UpdateCategoryCommandValidator();
            _fileMock = new Mock<IFormFile>();
        }

        [Fact]
        public async Task Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors()
        {
            var command = new UpdateCategoryCommand(1, "Нові роли", "New Rolls", 1.0, _fileMock.Object);

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Validate_WhenIdIsInvalid_ShouldHaveValidationErrorForId(int id)
        {
            var command = new UpdateCategoryCommand(id, "Роли", "Rolls", 1.0, _fileMock.Object);

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public async Task Validate_WhenTitleUaExceedsMaxLength_ShouldHaveValidationErrorForTitleUa()
        {
            var longTitle = new string('a', 101);
            var command = new UpdateCategoryCommand(1, longTitle, "Rolls", 1.0, _fileMock.Object);

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.TitleUa);
        }

        [Fact]
        public async Task Validate_WhenTitleEnExceedsMaxLength_ShouldHaveValidationErrorForTitleEn()
        {
            var longTitle = new string('a', 101);
            var command = new UpdateCategoryCommand(1, "Роли", longTitle, 1.0, _fileMock.Object);

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.TitleEn);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-5)]
        public async Task Validate_WhenSortOrderIsNegative_ShouldHaveValidationErrorForSortOrder(double sortOrder)
        {
            var command = new UpdateCategoryCommand(1, "Роли", "Rolls", sortOrder, _fileMock.Object);

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.SortOrder);
        }
    }
}