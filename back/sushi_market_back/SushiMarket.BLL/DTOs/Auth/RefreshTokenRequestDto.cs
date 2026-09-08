using System.Diagnostics.CodeAnalysis;

namespace SushiMarket.BLL.DTOs.Auth
{
    [ExcludeFromCodeCoverage]
    public class RefreshTokenRequestDto
    {
        required public string RefreshToken { get; set; }
    }
}