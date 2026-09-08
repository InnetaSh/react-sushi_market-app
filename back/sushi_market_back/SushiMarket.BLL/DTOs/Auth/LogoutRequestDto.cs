using System.Diagnostics.CodeAnalysis;

namespace SushiMarket.BLL.DTOs.Auth
{
    [ExcludeFromCodeCoverage]
    public record LogoutRequestDto(string RefreshToken);
}
