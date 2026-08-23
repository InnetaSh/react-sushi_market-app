using MediatR;
using SushiMarket.BLL.DTOs.Auth;

namespace SushiMarket.BLL.MediatR.Auth.Login
{
    public record LoginCommand(LoginDto Model) : IRequest<Unit>;
}