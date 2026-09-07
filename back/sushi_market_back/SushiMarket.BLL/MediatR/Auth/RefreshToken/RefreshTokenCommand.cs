using FluentResults;
using MediatR;
using SushiMarket.BLL.DTOs.Auth;

namespace SushiMarket.BLL.MediatR.Auth.RefreshToken
{
    public record RefreshTokenCommand(RefreshTokenRequestDto RefreshTokenRequest)
        : IRequest<Result<AuthResponseDto>>;
}