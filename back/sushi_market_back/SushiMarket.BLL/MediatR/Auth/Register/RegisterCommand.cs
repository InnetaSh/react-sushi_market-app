using FluentResults;
using MediatR;
using SushiMarket.BLL.DTOs.Auth;

namespace SushiMarket.BLL.MediatR.Auth.Register
{
    public record RegisterUserCommand(UserRegisterDto registerRequest)
    : IRequest<Result<AuthResponseDto>>;
}