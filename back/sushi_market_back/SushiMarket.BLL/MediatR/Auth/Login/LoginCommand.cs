using FluentResults;
using MediatR;
using SushiMarket.BLL.DTOs.Auth;

namespace SushiMarket.BLL.MediatR.Auth.Login
{
    public record LoginUserCommand(UserLoginDto loginRequest)
         : IRequest<Result<AuthResponseDto>>;
}