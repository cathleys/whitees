using Microsoft.AspNetCore.Identity;

namespace Whitees.Models;
public class AppUser : IdentityUser
{
    public string ProfileImageUrl { get; set; }
}
