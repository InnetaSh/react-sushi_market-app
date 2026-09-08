using Google.Apis.Auth;

namespace SushiMarket.BLL.Services.Interfaces.Users
{
    public interface IGoogleAuthService
    {
        Task<GoogleJsonWebSignature.Payload?> ValidateTokenAsync(string idToken);
    }
}
