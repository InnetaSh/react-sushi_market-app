using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.DAL.Entities.Users;

namespace SushiMarket.BLL.Services.Interfaces.Users
{
    public interface IAuthService
    {
        Task<AuthResponseDto> CreateLoginResultAsync(User user);
    }
}