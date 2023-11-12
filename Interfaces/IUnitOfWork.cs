using Whitees.Repositories;

namespace Whitees.Interfaces;
public interface IUnitOfWork
{
    IDashboardRepository DashboardRepository { get; }
    IOrderRepository OrderRepository { get; }
    IShirtRepository ShirtRepository { get; }
    IUserRepository UserRepository { get; }
}
