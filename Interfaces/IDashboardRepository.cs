using Whitees.Models;

namespace Whitees.Interfaces;
public interface IDashboardRepository
{

    Task<List<Shirt>> GetShirts();
    Task<AppUser> GetUserById(string userId);

}
