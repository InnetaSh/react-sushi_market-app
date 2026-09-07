using MediatR;
using FluentResults;
using SushiMarket.BLL.DTOs.Auth;

namespace SushiMarket.BLL.MediatR.Auth.LoginGoogle
{
    public record GoogleLoginCommand(GoogleLoginRequestDto googleLoginRequest)
      : IRequest<Result<AuthResponseDto>>;
}
