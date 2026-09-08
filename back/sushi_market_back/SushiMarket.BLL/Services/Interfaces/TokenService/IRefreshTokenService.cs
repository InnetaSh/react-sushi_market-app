using SushiMarket.DAL.Entities.Users;

namespace SushiMarket.BLL.Services.Interfaces.Users
{
    public interface IRefreshTokenService
    {
        Task<(User user, string newRefreshToken)> RefreshAsync(string refreshToken);
        Task SaveAsync(int userId, string refreshToken);
        Task RevokeAsync(string refreshToken);
        string Generate();
    }
}