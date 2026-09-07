using FluentResults;
using MediatR;

namespace SushiMarket.BLL.MediatR.Auth.Logout
{
    public record LogoutUserCommand(string RefreshToken) : IRequest<Result<Unit>>;
}
