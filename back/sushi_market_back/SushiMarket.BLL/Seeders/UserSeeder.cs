using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SushiMarket.DAL.Entities.Users;
using SushiMarket.DAL.Enums;

namespace SushiMarket.BLL.Seeders
{
    public static class UserSeeder
    {
        public static async Task FillSeedAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration)
        {
            // Создаем роли, если их нет
            foreach (var role in Enum.GetNames<UserRole>())
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }

            var adminEmail = configuration["AdminSettings:Email"] ?? "admin@sushimarket.com";
            var adminPassword = configuration["AdminSettings:Password"] ?? "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    Name = "Admin",
                    Surname = "SushiMaster",
                    Role = UserRole.MainAdministrator,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to create admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            if (!await userManager.IsInRoleAsync(adminUser, UserRole.MainAdministrator.ToString()))
            {
                await userManager.AddToRoleAsync(adminUser, UserRole.MainAdministrator.ToString());
            }
        }
    }
}