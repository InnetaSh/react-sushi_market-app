using FluentValidation.TestHelper;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.Validators.Auth;
using Xunit;

namespace SushiMarket.Tests.Validators.Auth
{
    public class LoginDtoValidatorTests
    {
        private readonly LoginDtoValidator _validator;

        public LoginDtoValidatorTests()
        {
            _validator = new LoginDtoValidator();
        }

        [Fact]
        public async Task Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors()
        {
            // Arrange
            var model = new LoginDto
            {
                Email = "test@example.com",
                Password = "SecurePassword123"
            };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@example.com")]
        public async Task Validate_WhenEmailIsInvalid_ShouldHaveValidationErrorForEmail(string? email)
        {
            // Arrange
            var model = new LoginDto
            {
                Email = email!,
                Password = "SecurePassword123"
            };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("12345")] // < 6 chars
        public async Task Validate_WhenPasswordIsInvalid_ShouldHaveValidationErrorForPassword(string? password)
        {
            // Arrange
            var model = new LoginDto
            {
                Email = "test@example.com",
                Password = password!
            };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }
}