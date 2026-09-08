using SushiMarket.DAL.Entities.Users;

namespace SushiMarket.BLL.Extensions
{
    public static class UserExtensions
    {
        public static void EnsureSecurityStamp(this User user)
        {
            if (string.IsNullOrEmpty(user.SecurityStamp))
            {
                user.SecurityStamp = Guid.NewGuid().ToString();
            }
        }
    }
}
