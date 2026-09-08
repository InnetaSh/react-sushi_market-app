using SushiMarket.DAL.Entities.Users;
using System.IdentityModel.Tokens.Jwt;

namespace SushiMarket.BLL.Services.Interfaces.Users
{
    public interface IJwtTokenService
    {
        JwtSecurityToken GenerateToken(User user);
    }
}
