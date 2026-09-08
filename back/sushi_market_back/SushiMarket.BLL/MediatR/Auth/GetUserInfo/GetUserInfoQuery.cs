using FluentResults;
using MediatR;
using SushiMarket.BLL.DTOs.Auth;

namespace SushiMarket.BLL.MediatR.Auth.GetUserInfo
{
    public record GetUserInfoQuery(int UserId) : IRequest<Result<UserProfileDto>>;
}