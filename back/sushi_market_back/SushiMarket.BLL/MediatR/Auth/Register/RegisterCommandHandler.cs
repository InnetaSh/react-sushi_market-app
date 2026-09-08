using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.Extensions;
using SushiMarket.BLL.Services.Interfaces.Logging;
using SushiMarket.BLL.Services.Interfaces.Users;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities.Users;
using SushiMarket.DAL.Enums;

namespace SushiMarket.BLL.MediatR.Auth.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<AuthResponseDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IAuthService _authService;
        private readonly SushiMarketDbContext _context;

        public RegisterUserHandler(
         UserManager<User> userManager,
         IMapper mapper,
         ILoggerService logger,
         IAuthService authService,
        SushiMarketDbContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _authService = authService;
            _context = context;
        }

        public async Task<Result<AuthResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Register attempt for {request.registerRequest.Email}");

            var existingUser = await _userManager.FindByEmailAsync(request.registerRequest.Email);
            if (existingUser != null)
            {
                return Result.Fail<AuthResponseDto>("User already exists");
            }

            var user = _mapper.Map<User>(request.registerRequest);
            user.UserName = request.registerRequest.Email;
            user.EnsureSecurityStamp();

            var result = await _userManager.CreateAsync(user, request.registerRequest.Password);
            if (!result.Succeeded)
            {
                return Result.Fail<AuthResponseDto>(result.Errors.Select(e => e.Description));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, UserRole.User.ToString());
            if (!roleResult.Succeeded)
            {
                return Result.Fail<AuthResponseDto>(roleResult.Errors.Select(e => e.Description));
            }

            //await _context.SaveChangesAsync(cancellationToken);

            var registrResult = await _authService.CreateLoginResultAsync(user);

            return Result.Ok(registrResult);
        }
    }
}