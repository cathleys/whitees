using Microsoft.EntityFrameworkCore;
using Whitees.Data;
using Whitees.Interfaces;
using Whitees.Models;

namespace Whitees.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppUser>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
}
