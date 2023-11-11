using Whitees.Models;

namespace Whitees.Interfaces;
public interface IUserRepository
{
    Task<IEnumerable<AppUser>> GetUsers();

}
