using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.BLL.Services.Interfaces.Users;
using SushiMarket.DAL.Entities.Users;
using System.IdentityModel.Tokens.Jwt;

namespace SushiMarket.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwt;
        private readonly IRefreshTokenService _refresh;
        private readonly IMapper _mapper;

        public AuthService(
            IJwtTokenService jwt,
            IRefreshTokenService refresh,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _jwt = jwt;
            _refresh = refresh;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> CreateLoginResultAsync(User user)
        {
            var jwt = _jwt.GenerateToken(user);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            var refreshToken = _refresh.Generate();
            await _refresh.SaveAsync(user.Id, refreshToken);

            return new AuthResponseDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = accessToken,
                RefreshToken = refreshToken,
                ExpireAt = jwt.ValidTo
            };
        }
    }
}