using Himchistka.Data.Identity;
using Himchistka.Data;
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

        public static async Task SeedSadmin(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {

                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "prachka.prachka@yandex.ru",
                    FullName = "Админ Админович"
                };
                IdentityResult result = userManager.CreateAsync(user, "superpassword123!").Result; 
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

    }
}
