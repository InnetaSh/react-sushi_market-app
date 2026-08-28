using FluentValidation.TestHelper;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.Validators.Auth;
using Xunit;

namespace SushiMarket.Tests.Validators.Auth
{
    public class RegisterDtoValidatorTests
    {
        private readonly RegisterDtoValidator _validator;

        public RegisterDtoValidatorTests()
        {
            _validator = new RegisterDtoValidator();
        }

        [Fact]
        public async Task Validate_WhenModelIsValid_ShouldNotHaveAnyValidationErrors()
        {
            // Arrange
            var model = new RegisterDto
            {
                Email = "test@example.com",
                Password = "SecurePassword123",
                Name = "Іван",
                Surname = "Петренко"
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
            var model = new RegisterDto
            {
                Email = email!,
                Password = "SecurePassword123",
                Name = "Іван",
                Surname = "Петренко"
            };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public async Task Validate_WhenEmailExceedsMaxLength_ShouldHaveValidationErrorForEmail()
        {
            // Arrange
            var longEmail = new string('a', 91) + "@example.com"; // > 100 chars
            var model = new RegisterDto
            {
                Email = longEmail,
                Password = "SecurePassword123",
                Name = "Іван",
                Surname = "Петренко"
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
            var model = new RegisterDto
            {
                Email = "test@example.com",
                Password = password!,
                Name = "Іван",
                Surname = "Петренко"
            };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Validate_WhenNameIsEmpty_ShouldHaveValidationErrorForName(string? name)
        {
            // Arrange
            var model = new RegisterDto
            {
                Email = "test@example.com",
                Password = "SecurePassword123",
                Name = name!,
                Surname = "Петренко"
            };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Validate_WhenSurnameIsEmpty_ShouldHaveValidationErrorForSurname(string? surname)
        {
            // Arrange
            var model = new RegisterDto
            {
                Email = "test@example.com",
                Password = "SecurePassword123",
                Name = "Іван",
                Surname = surname!
            };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }
    }
}