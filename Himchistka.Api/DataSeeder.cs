using Microsoft.AspNetCore.Identity;

namespace Himchistka.Api
{
    public class DataSeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

    }
}
