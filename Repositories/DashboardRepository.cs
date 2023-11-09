using Microsoft.EntityFrameworkCore;
using Whitees.Data;
using Whitees.Extensions;
using Whitees.Interfaces;
using Whitees.Models;

namespace Whitees.Repositories;
public class DashboardRepository : IDashboardRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;

    }

    public async Task<List<Shirt>> GetShirts()
    {
        var user = _httpContextAccessor.HttpContext.User.GetUserId();

        var userShirts = _context.Shirts
        .Where(s => s.AppUser.Id == user);

        return await userShirts.ToListAsync();
    }

    public async Task<AppUser> GetUserById(string userId)
    {
        return await _context.Users.FindAsync(userId);

    }
}
