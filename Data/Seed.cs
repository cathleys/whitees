using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Whitees.Models;

namespace Whitees.Data;
public class Seed
{

    public static async Task SeedUsers(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            //Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (await userManager.Users.AnyAsync()) return;

            //Admins

            var admin = new AppUser
            {
                UserName = "cathdev",
                Email = "cathdeveloper@gmail.com",
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRoleAsync(admin, UserRoles.Admin);


            var admin1 = new AppUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(admin1, "Pa$$w0rd");
            await userManager.AddToRoleAsync(admin1, UserRoles.Admin);


            //member
            var member = new AppUser
            {
                UserName = "member",
                Email = "user@gmail.com",
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(member, "Pa$$w0rd");
            await userManager.AddToRoleAsync(member, UserRoles.User);


        }
    }
}
