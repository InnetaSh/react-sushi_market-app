using MediatR;
using SushiMarket.BLL.DTOs.Auth;

namespace SushiMarket.BLL.MediatR.Auth.Register
{
    public record RegisterCommand(RegisterDto Model) : IRequest<Unit>;
}