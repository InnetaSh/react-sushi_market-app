namespace SushiMarket.BLL.DTOs.Auth
{
    public class UserRegisterDto
    {
        required public string Name { get; set; }

        required public string Surname { get; set; }

        required public string Email { get; set; }

        required public string Password { get; set; }

    }
}
