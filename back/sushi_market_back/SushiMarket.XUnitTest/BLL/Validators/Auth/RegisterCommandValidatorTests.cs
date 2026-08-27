using FluentAssertions;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.MediatR.Auth.Register;

namespace SushiMarket.Tests.Validators.Auth
{
    public class RegisterCommandValidatorTests
    {
        private readonly RegisterCommandValidator _validator;

        public RegisterCommandValidatorTests()
        {
            _validator = new RegisterCommandValidator();
        }

        [Fact]
        public void Validate_WhenModelIsValid_ShouldNotHaveErrors()
        {
            // Arrange
            var model = new RegisterDto
            {
                Email = "test@example.com",
                Password = "Password123!",
                Name = "Ivan",
                Surname = "Petrenko"
            };

            var command = new RegisterCommand(model);

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
            var model = new RegisterDto
            {
                Email = "",
                Password = "",
                Name = "",
                Surname = ""
            };

            var command = new RegisterCommand(model);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public void Validate_WhenModelIsNull_ShouldHaveValidationError()
        {
            // Arrange
            var command = new RegisterCommand(null);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();

            result.Errors.Should().Contain(error =>
                error.PropertyName == "Model");
        }
    }
}