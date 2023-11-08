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

            //Admins
            string adminEmail = "cathdeveloper@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new AppUser
                {
                    UserName = "cathdev",
                    Email = adminEmail,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(newAdmin, "Pa$$w0rd");
                await userManager.AddToRoleAsync(newAdmin, UserRoles.Admin);
            }

            string adminEmail1 = "admin@gmail.com";
            var adminUser1 = await userManager.FindByEmailAsync(adminEmail1);
            if (adminUser1 == null)
            {
                var newAdmin1 = new AppUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(newAdmin1, "Pa$$w0rd");
                await userManager.AddToRoleAsync(newAdmin1, UserRoles.Admin);
            }

            //member
            string memberEmail = "user@gmail.com";
            var member = await userManager.FindByEmailAsync(memberEmail);
            if (member == null)
            {
                var newMember = new AppUser
                {
                    UserName = "member",
                    Email = adminEmail,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(newMember, "Pa$$w0rd");
                await userManager.AddToRoleAsync(newMember, UserRoles.User);
            }

        }
    }
}
