using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.MediatR.Auth.Login;
using SushiMarket.DAL.Entities.Users;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class LoginCommandHandlerTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        var store = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        _handler = new LoginCommandHandler(_userManagerMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCredentials_ShouldSucceed()
    {
        // Arrange
        var command = new LoginCommand(new LoginDto
        {
            Email = "test@sushimarket.com",
            Password = "ValidPassword123!"
        });

        var user = new User { Email = command.Model.Email };

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(command.Model.Email))
            .ReturnsAsync(user);

        _userManagerMock
            .Setup(x => x.CheckPasswordAsync(user, command.Model.Password))
            .ReturnsAsync(true);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Handle_WithNonExistentEmail_ShouldThrowUnauthorizedAccessException()
    {
        // Arrange
        var command = new LoginCommand(new LoginDto
        {
            Email = "wrong@sushimarket.com",
            Password = "Password123!"
        });

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(command.Model.Email))
            .ReturnsAsync((User?)null);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Invalid email or password.");
    }

    [Fact]
    public async Task Handle_WithInvalidPassword_ShouldThrowUnauthorizedAccessException()
    {
        // Arrange
        var command = new LoginCommand(new LoginDto
        {
            Email = "test@sushimarket.com",
            Password = "WrongPassword!"
        });

        var user = new User { Email = command.Model.Email };

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(command.Model.Email))
            .ReturnsAsync(user);

        _userManagerMock
            .Setup(x => x.CheckPasswordAsync(user, command.Model.Password))
            .ReturnsAsync(false);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Invalid email or password.");
    }
}