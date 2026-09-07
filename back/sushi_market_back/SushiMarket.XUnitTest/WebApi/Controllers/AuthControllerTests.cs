using FluentAssertions;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.MediatR.Auth.Login;
using SushiMarket.BLL.MediatR.Auth.Logout;
using SushiMarket.BLL.MediatR.Auth.Register;
using SushiMarket.BLL.Services.Interfaces.Users;
using SushiMarket.WebAPI.Controllers;
using Xunit;

namespace SushiMarket.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IGoogleAuthService> _googleAuthServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _googleAuthServiceMock = new Mock<IGoogleAuthService>();

            _controller = new AuthController(
                _mediatorMock.Object,
                _googleAuthServiceMock.Object);
        }

        [Fact]
        public async Task Register_WhenValidDto_ReturnsOkResult()
        {
            var dto = new UserRegisterDto
            {
                Name = "Test",
                Surname = "User",
                Email = "test@test.com",
                Password = "Password123!",
                PasswordConfirmation = "Password123!"
            };

            var authResponse = new AuthResponseDto
            {
                User = new UserDto(),
                Token = "fake-jwt-token",
                RefreshToken = "fake-refresh-token",
                ExpireAt = DateTime.UtcNow.AddHours(1)
            };

            var expectedResponse = Result.Ok(authResponse);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var result = await _controller.Register(dto);

            result.Should().BeOfType<OkObjectResult>();

            _mediatorMock.Verify(
                m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Login_WhenValidDto_ReturnsOkResult()
        {
            var dto = new UserLoginDto
            {
                Login = "test@test.com",
                Password = "Password123!"
            };

            var authResponse = new AuthResponseDto
            {
                User = new UserDto(),
                Token = "fake-jwt-token",
                RefreshToken = "fake-refresh-token",
                ExpireAt = DateTime.UtcNow.AddHours(1)
            };

            var expectedResponse = Result.Ok(authResponse);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var result = await _controller.Login(dto);

            result.Should().BeOfType<OkObjectResult>();

            _mediatorMock.Verify(
                m => m.Send(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Logout_WhenValidRefreshToken_ReturnsNoContentResult()
        {
            var refreshToken = "valid-refresh-token";
            var expectedResponse = Result.Ok(Unit.Value);

            _mediatorMock
                .Setup(m => m.Send(
                    It.IsAny<LogoutUserCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var result = await _controller.Logout(refreshToken);

            result.Should().BeOfType<NoContentResult>();

            _mediatorMock.Verify(
                m => m.Send(
                    It.IsAny<LogoutUserCommand>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Logout_WhenRefreshTokenMissing_ReturnsBadRequest()
        {
            var refreshToken = string.Empty;

            var result = await _controller.Logout(refreshToken);

            result.Should().BeOfType<BadRequestObjectResult>();

            _mediatorMock.Verify(
                m => m.Send(It.IsAny<LogoutUserCommand>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}