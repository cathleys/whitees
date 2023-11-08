using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Whitees.Data;
using Whitees.Models;

namespace Whitees.Extensions;
public static class IdentityServiceExtensions
{

    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<DataContext>();

        services.AddMemoryCache();
        services.AddSession();

        services.AddAuthentication(
            CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        return services;
    }
}
