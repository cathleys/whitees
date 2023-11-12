using Whitees.Data;
using Whitees.Interfaces;

namespace Whitees.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UnitOfWork(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    public IDashboardRepository DashboardRepository => new DashboardRepository(_context, _httpContextAccessor);

    public IOrderRepository OrderRepository => new OrderRepository(_context, _httpContextAccessor);

    public IShirtRepository ShirtRepository => new ShirtRepository(_context);

    public IUserRepository UserRepository => new UserRepository(_context);

}
