using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using SushiMarket.BLL.Services.Interfaces.Users;

namespace SushiMarket.BLL.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly IConfiguration _configuration;

        public GoogleAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GoogleJsonWebSignature.Payload?> ValidateTokenAsync(string idToken)
        {
            try
            {
                var clientId = _configuration["GoogleAuth:ClientId"];
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string> { clientId! }
                };

                return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            }
            catch
            {
                return null;
            }
        }
    }
}
