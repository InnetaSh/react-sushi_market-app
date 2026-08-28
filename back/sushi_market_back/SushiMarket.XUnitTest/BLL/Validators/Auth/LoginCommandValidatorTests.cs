using FluentAssertions;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.MediatR.Auth.Login;

namespace SushiMarket.Tests.Validators.Auth
{
    public class LoginCommandValidatorTests
    {
        private readonly LoginCommandValidator _validator;

        public LoginCommandValidatorTests()
        {
            _validator = new LoginCommandValidator();
        }

        [Fact]
        public void Validate_WhenModelIsValid_ShouldNotHaveErrors()
        {
            // Arrange
            var model = new LoginDto
            {
                Email = "test@example.com",
                Password = "Password123!"
            };

            var command = new LoginCommand(model);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_WhenModelIsInvalid_ShouldHaveErrors()
        {
            // Arrange
            var model = new LoginDto
            {
                Email = "",
                Password = ""
            };

            var command = new LoginCommand(model);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public void Validate_WhenEmailIsInvalid_ShouldHaveValidationError()
        {
            // Arrange
            var model = new LoginDto
            {
                Email = "invalid-email",
                Password = "Password123!"
            };

            var command = new LoginCommand(model);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName.Contains("Email"));
        }

        [Fact]
        public void Validate_WhenPasswordIsEmpty_ShouldHaveValidationError()
        {
            // Arrange
            var model = new LoginDto
            {
                Email = "test@example.com",
                Password = ""
            };

            var command = new LoginCommand(model);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName.Contains("Password"));
        }

        [Fact]
        public void Validate_WhenModelIsNull_ShouldHaveValidationError()
        {
            // Arrange
            var command = new LoginCommand(null);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Model");
        }
    }
}