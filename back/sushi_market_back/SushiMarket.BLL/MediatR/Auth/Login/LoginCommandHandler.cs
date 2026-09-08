using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.Extensions;
using SushiMarket.BLL.Services.Interfaces.Logging;
using SushiMarket.BLL.Services.Interfaces.Users;
using SushiMarket.DAL.Entities.Users;

namespace SushiMarket.BLL.MediatR.Auth.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<AuthResponseDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILoggerService _logger;
        private readonly IAuthService _authService;

        public LoginUserHandler(
            UserManager<User> userManager,
            IMapper mapper,
            ILoggerService logger,
            IAuthService authService)
        {
            _userManager = userManager;
            _logger = logger;
            _authService = authService;
        }

        public async Task<Result<AuthResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Login attempt for {request.loginRequest.Login}");

            var user = await _userManager.FindByNameAsync(request.loginRequest.Login);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.loginRequest.Password))
            {
                _logger.LogError(request, $"Failed login attempt for {request.loginRequest.Login}");
                return Result.Fail<AuthResponseDto>("Invalid login or password.");
            }
            user.EnsureSecurityStamp();

            var loginResult = await _authService.CreateLoginResultAsync(user);

            _logger.LogInformation($"User {user.Id} successfully logged in");

            return Result.Ok(loginResult);
        }
    }
}