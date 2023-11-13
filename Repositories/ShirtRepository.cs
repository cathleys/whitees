using System.Linq;
using cloudscribe.Pagination.Models;
using Microsoft.EntityFrameworkCore;
using Whitees.Data;
using Whitees.Helpers;
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

    public async Task<IEnumerable<Shirt>> GetAllShirts(string sortShirts)
    {
        return await _context.Shirts.ToListAsync();

    }

    public async Task<PagedResult<Shirt>> GetPaginatedShirts(UserParams userParams)
    {
        var query = _context.Shirts.AsQueryable();
        var shirtCount = await query.CountAsync();


        //filter on ShirtSale
        if (userParams.ShirtSale.HasValue)
        {
            int shirtSaleValue = (int)userParams.ShirtSale.Value;
            query = query.Where(s => (int)s.ShirtSale == shirtSaleValue);
        }

        //filter on searchbar
        if (!string.IsNullOrEmpty(userParams.searchString))
        {
            query = query.Where(s => s.Name.Contains(userParams.searchString));
            _ = shirtCount;
        }

        //pagination
        var excludeItems = (userParams.PageSize * userParams.PageNumber) - userParams.PageSize;
        query = query.Skip(excludeItems).Take(userParams.PageSize);

        var result = new PagedResult<Shirt>
        {
            Data = await query.AsNoTracking().ToListAsync(),
            TotalItems = shirtCount,
            PageNumber = userParams.PageNumber,
            PageSize = userParams.PageSize,
        };

        return result;

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
