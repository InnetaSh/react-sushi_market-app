namespace SushiMarket.BLL.DTOs.Auth
{
    public class UserLoginDto
    {
        required public string Login { get; set; }

        required public string Password { get; set; }
    }
}
