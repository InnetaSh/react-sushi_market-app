using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.MediatR.Auth.Register;
using SushiMarket.BLL.Resources;
using SushiMarket.DAL.Entities.Users;
using SushiMarket.DAL.Enums;

public class RegisterCommandHandlerTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTests()
    {
        var store = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        _handler = new RegisterCommandHandler(_userManagerMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldRegisterUserSuccessfully()
    {
        // Arrange
        var command = new RegisterCommand(new RegisterDto
        {
            Email = "newuser@sushimarket.com",
            Password = "StrongPassword123!",
            Name = "John",
            Surname = "Doe"
        });

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(command.Model.Email))
            .ReturnsAsync((User?)null);

        _userManagerMock
            .Setup(x => x.CreateAsync(It.IsAny<User>(), command.Model.Password))
            .ReturnsAsync(IdentityResult.Success);

        _userManagerMock
            .Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();

        _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<User>(), command.Model.Password), Times.Once);
        _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<User>(), UserRole.User.ToString()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithExistingEmail_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var command = new RegisterCommand(new RegisterDto
        {
            Email = "existing@sushimarket.com",
            Password = "Password123!",
            Name = "Jane",
            Surname = "Doe"
        });

        var existingUser = new User { Email = command.Model.Email };

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(command.Model.Email))
            .ReturnsAsync(existingUser);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage(ErrorMessages.UserAlreadyExists);
    }

    [Fact]
    public async Task Handle_WhenCreationFails_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var command = new RegisterCommand(new RegisterDto
        {
            Email = "user@sushimarket.com",
            Password = "123",
            Name = "Test",
            Surname = "User"
        });

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(command.Model.Email))
            .ReturnsAsync((User?)null);

        var identityError = new IdentityError { Description = "Password too short." };
        _userManagerMock
            .Setup(x => x.CreateAsync(It.IsAny<User>(), command.Model.Password))
            .ReturnsAsync(IdentityResult.Failed(identityError));

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage(string.Format(ErrorMessages.RegistrationFailed, "Password too short."));
    }
}