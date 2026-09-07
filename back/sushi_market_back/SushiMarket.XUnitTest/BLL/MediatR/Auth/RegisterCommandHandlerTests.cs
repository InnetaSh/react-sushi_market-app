using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.MediatR.Auth.Register;
using SushiMarket.BLL.Services.Interfaces.Logging;
using SushiMarket.BLL.Services.Interfaces.Users;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities.Users;
using SushiMarket.DAL.Enums;

namespace SushiMarket.Tests.MediatR.Auth
{
    public class RegisterCommandHandlerTests
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<ILoggerService> _loggerMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<SushiMarketDbContext> _contextMock;
        private readonly IMapper _mapper;
        private readonly RegisterUserHandler _handler;

        public RegisterCommandHandlerTests()
        {
            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null!, null!, null!, null!, null!, null!, null!, null!);

            _loggerMock = new Mock<ILoggerService>();
            _authServiceMock = new Mock<IAuthService>();

            _contextMock = new Mock<SushiMarketDbContext>(
                new DbContextOptions<SushiMarketDbContext>());

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserRegisterDto, User>();
            }, NullLoggerFactory.Instance);

            _mapper = config.CreateMapper();

            _handler = new RegisterUserHandler(
                _userManagerMock.Object,
                _mapper,
                _loggerMock.Object,
                _authServiceMock.Object,
                _contextMock.Object);
        }

        private static UserRegisterDto CreateValidDto() => new()
        {
            Name = "John",
            Surname = "Doe",
            Email = "john@test.com",
            Password = "Password123!",
            PasswordConfirmation = "Password123!"
        };

        private static RegisterUserCommand CreateCommand(UserRegisterDto dto)
            => new(dto);

        private static User CreateTestUser() => new()
        {
            Id = 1,
            UserName = "testUser",
            Name = "John",
            Surname = "Doe"
        };

        [Fact]
        public async Task Handle_ShouldRegisterUser_WhenDataIsValid()
        {
            var dto = CreateValidDto();

            _userManagerMock.Setup(x => x.FindByEmailAsync(dto.Email))
                .ReturnsAsync((User?)null);

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), dto.Password))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), UserRole.User.ToString()))
                .ReturnsAsync(IdentityResult.Success);

            _authServiceMock.Setup(x => x.CreateLoginResultAsync(It.IsAny<User>()))
                .ReturnsAsync(new AuthResponseDto
                {
                    Token = "jwt-token",
                    RefreshToken = "refresh-token",
                    User = new UserDto
                    {
                        Email = dto.Email,
                        Name = dto.Name,
                        Surname = dto.Surname
                    }
                });

            _contextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var result = await _handler.Handle(CreateCommand(dto), CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Token.Should().Be("jwt-token");

            _userManagerMock.Verify(
                x => x.CreateAsync(It.IsAny<User>(), dto.Password),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenUserAlreadyExists()
        {
            var dto = CreateValidDto();
            var user = CreateTestUser();

            _userManagerMock.Setup(x => x.FindByEmailAsync(dto.Email))
                .ReturnsAsync(user);

            var result = await _handler.Handle(CreateCommand(dto), CancellationToken.None);

            result.IsFailed.Should().BeTrue();
            result.Errors.Select(e => e.Message).Should().Contain("User already exists");
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenCreateUserFails()
        {
            var dto = CreateValidDto();

            _userManagerMock.Setup(x => x.FindByEmailAsync(dto.Email))
                .ReturnsAsync((User?)null);

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), dto.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError
                {
                    Description = "Password too weak"
                }));

            var result = await _handler.Handle(CreateCommand(dto), CancellationToken.None);

            result.IsFailed.Should().BeTrue();
            result.Errors.Select(e => e.Message).Should().Contain("Password too weak");
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenAddToRoleFails()
        {
            var dto = CreateValidDto();

            _userManagerMock.Setup(x => x.FindByEmailAsync(dto.Email))
                .ReturnsAsync((User?)null);

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), dto.Password))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError
                {
                    Description = "Role assignment error"
                }));

            var result = await _handler.Handle(CreateCommand(dto), CancellationToken.None);

            result.IsFailed.Should().BeTrue();
            result.Errors.Select(e => e.Message).Should().Contain("Role assignment error");
        }
    }
}