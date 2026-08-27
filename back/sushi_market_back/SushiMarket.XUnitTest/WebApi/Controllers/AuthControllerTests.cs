using System.Security.Claims;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.MediatR.Auth.Login;
using SushiMarket.BLL.MediatR.Auth.Register;
using SushiMarket.DAL.Entities.Users;
using SushiMarket.WebAPI.Controllers;
using Xunit;

namespace SushiMarket.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<SignInManager<User>> _signInManagerMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();

            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(
                userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<User>>();
            var optionsMock = new Mock<Microsoft.Extensions.Options.IOptions<IdentityOptions>>();
            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<SignInManager<User>>>();
            var schemesMock = new Mock<IAuthenticationSchemeProvider>();
            var confirmationMock = new Mock<IUserConfirmation<User>>();

            _signInManagerMock = new Mock<SignInManager<User>>(
                _userManagerMock.Object,
                contextAccessorMock.Object,
                claimsFactoryMock.Object,
                optionsMock.Object,
                loggerMock.Object,
                schemesMock.Object,
                confirmationMock.Object);

            _controller = new AuthController(
                _mediatorMock.Object,
                _signInManagerMock.Object,
                _userManagerMock.Object);
        }

        [Fact]
        public async Task Register_WhenValidDto_ReturnsOkResult()
        {
            // Arrange
            var dto = new RegisterDto { Email = "test@test.com", Password = "Password123!", Name = "Test", Surname = "User" };
            var user = new User { Email = dto.Email, Name = dto.Name, Surname = dto.Surname };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            _userManagerMock
                .Setup(um => um.FindByEmailAsync(dto.Email))
                .ReturnsAsync(user);

            _signInManagerMock
                .Setup(sm => sm.SignInAsync(user, true, null))
                .Returns(Task.CompletedTask);

            _userManagerMock
                .Setup(um => um.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Register_WhenInvalidOperationException_ReturnsBadRequest()
        {
            // Arrange
            var dto = new RegisterDto { Email = "test@test.com", Password = "Password123!" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("User already exists"));

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_WhenValidDto_ReturnsOkResult()
        {
            // Arrange
            var dto = new LoginDto { Email = "test@test.com", Password = "Password123!" };
            var user = new User { Email = dto.Email, Name = "Test", Surname = "User" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            _userManagerMock
                .Setup(um => um.FindByEmailAsync(dto.Email))
                .ReturnsAsync(user);

            _signInManagerMock
                .Setup(sm => sm.SignInAsync(user, true, null))
                .Returns(Task.CompletedTask);

            _userManagerMock
                .Setup(um => um.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_WhenUserNotFound_ReturnsUnauthorized()
        {
            // Arrange
            var dto = new LoginDto { Email = "notfound@test.com", Password = "Password123!" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            _userManagerMock
                .Setup(um => um.FindByEmailAsync(dto.Email))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
        }

        [Fact]
        public async Task Logout_ReturnsOkResult()
        {
            // Arrange
            _signInManagerMock
                .Setup(sm => sm.SignOutAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Logout();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetUserInfo_WhenAuthenticated_ReturnsUserData()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "test@test.com"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = _controller.GetUserInfo();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().NotBeNull();
        }
    }
}