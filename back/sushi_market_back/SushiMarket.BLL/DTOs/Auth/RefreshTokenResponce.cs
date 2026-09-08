namespace SushiMarket.BLL.DTOs.Auth
{
    public class RefreshTokenResponce
    {
        required public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
