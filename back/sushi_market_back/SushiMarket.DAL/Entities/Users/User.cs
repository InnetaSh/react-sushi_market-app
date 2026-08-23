using Microsoft.AspNetCore.Identity;
using SushiMarket.DAL.Enums;

namespace SushiMarket.DAL.Entities.Users
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
    }
}