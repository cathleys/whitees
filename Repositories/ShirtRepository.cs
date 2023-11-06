using Microsoft.EntityFrameworkCore;
using Whitees.Data;
using Whitees.Interfaces;
using Whitees.Models;

namespace Whitees.Repositories;
public class ShirtRepository : IShirtRepository
{
    private readonly DataContext _context;

    public ShirtRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> Add(Shirt shirt)
    {
        _context.Add(shirt);
        return await Save();
    }

    public async Task<bool> Delete(Shirt shirt)
    {
        _context.Remove(shirt);
        return await Save();
    }

    public async Task<IEnumerable<Shirt>> GetAllShirts()
    {
        return await _context.Shirts.ToListAsync();

    }

    public async Task<Shirt> GetShirtById(int id)
    {
        return await _context.Shirts.FindAsync(id);
    }

    public async Task<Shirt> GetShirtByIdNoTracking(int id)
    {
        return await _context.Shirts
        .AsNoTracking()
        .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> Save()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Shirt shirt)
    {
        _context.Update(shirt);
        return await Save();
    }
}
