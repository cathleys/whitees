using System.Security.Claims;

namespace Whitees.Extensions;
public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
    public static string GetUserRole(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.Role);
    }
}
