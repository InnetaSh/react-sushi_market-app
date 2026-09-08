using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.DAL.Entities.Users;

namespace SushiMarket.BLL.MediatR.Auth.GetUserInfo
{
    public class GetUserInfoHandler : IRequestHandler<GetUserInfoQuery, Result<UserProfileDto>>
    {
        private readonly UserManager<User> _userManager;

        public GetUserInfoHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<UserProfileDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                return Result.Fail("User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userProfile = new UserProfileDto
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Role = roles.FirstOrDefault() ?? "User"
            };

            return Result.Ok(userProfile);
        }
    }
}