using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using SushiMarket.BLL.MediatR.Behaviors;

namespace SushiMarket.Tests.MediatR.Behaviors
{
    public class ValidationBehaviorTests
    {
        public class TestRequest : IRequest<Unit>
        {
            public string Name { get; set; } = string.Empty;
        }

        [Fact]
        public async Task Handle_WhenNoValidators_ShouldCallNext()
        {
            // Arrange
            var validators = new List<IValidator<TestRequest>>();
            var behavior = new ValidationBehavior<TestRequest, Unit>(validators);
            var request = new TestRequest();

            var nextCalled = false;
            RequestHandlerDelegate<Unit> next = _ =>
            {
                nextCalled = true;
                return Task.FromResult(Unit.Value);
            };

            // Act
            var result = await behavior.Handle(request, next, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            nextCalled.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_WhenValidationFails_ShouldThrowValidationException()
        {
            // Arrange
            var validatorMock = new Mock<IValidator<TestRequest>>();
            validatorMock
                .Setup(v => v.ValidateAsync(
                    It.IsAny<ValidationContext<TestRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("Name", "Required")
                }));

            var validators = new List<IValidator<TestRequest>> { validatorMock.Object };
            var behavior = new ValidationBehavior<TestRequest, Unit>(validators);
            var request = new TestRequest();

            var nextCalled = false;
            RequestHandlerDelegate<Unit> next = _ =>
            {
                nextCalled = true;
                return Task.FromResult(Unit.Value);
            };

            // Act
            Func<Task> act = () => behavior.Handle(request, next, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
            nextCalled.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_WhenValidationPasses_ShouldCallNext()
        {
            // Arrange
            var validatorMock = new Mock<IValidator<TestRequest>>();
            validatorMock
                .Setup(v => v.ValidateAsync(
                    It.IsAny<ValidationContext<TestRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var validators = new List<IValidator<TestRequest>> { validatorMock.Object };
            var behavior = new ValidationBehavior<TestRequest, Unit>(validators);
            var request = new TestRequest();

            var nextCalled = false;
            RequestHandlerDelegate<Unit> next = _ =>
            {
                nextCalled = true;
                return Task.FromResult(Unit.Value);
            };

            // Act
            var result = await behavior.Handle(request, next, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            nextCalled.Should().BeTrue();
        }
    }
}