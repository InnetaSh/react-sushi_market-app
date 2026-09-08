using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;
using SushiMarket.BLL.MediatR.Categories.CreateCategory;
using Xunit;

namespace SushiMarket.Tests.Validators.Categories
{
    public class CreateCategoryCommandValidatorTests
    {
        private readonly CreateCategoryCommandValidator _validator;
        private readonly Mock<IFormFile> _fileMock;

        public CreateCategoryCommandValidatorTests()
        {
            _validator = new CreateCategoryCommandValidator();
            _fileMock = new Mock<IFormFile>();
        }

        [Fact]
        public async Task Validate_WhenAtLeastOneTitleProvided_ShouldNotHaveAnyValidationErrors()
        {
            var commandWithUa = new CreateCategoryCommand("Роли", null!, _fileMock.Object, 1.0);
            var commandWithEn = new CreateCategoryCommand(null!, "Rolls", _fileMock.Object, 1.0);
            var commandWithBoth = new CreateCategoryCommand("Роли", "Rolls", _fileMock.Object, 1.0);

            (await _validator.TestValidateAsync(commandWithUa)).ShouldNotHaveAnyValidationErrors();
            (await _validator.TestValidateAsync(commandWithEn)).ShouldNotHaveAnyValidationErrors();
            (await _validator.TestValidateAsync(commandWithBoth)).ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("    ", "    ")]
        public async Task Validate_WhenBothTitlesAreMissing_ShouldHaveValidationError(string? titleUa, string? titleEn)
        {
            var command = new CreateCategoryCommand(titleUa!, titleEn!, _fileMock.Object, 1.0);

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public async Task Validate_WhenTitleUaExceedsMaxLength_ShouldHaveValidationErrorForTitleUa()
        {
            var longTitle = new string('a', 101);
            var command = new CreateCategoryCommand(longTitle, null!, _fileMock.Object, 1.0);

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.TitleUa);
        }

        [Fact]
        public async Task Validate_WhenTitleEnExceedsMaxLength_ShouldHaveValidationErrorForTitleEn()
        {
            var longTitle = new string('a', 101);
            var command = new CreateCategoryCommand(null!, longTitle, _fileMock.Object, 1.0);

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.TitleEn);
        }
    }
}