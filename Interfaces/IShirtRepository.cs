using Whitees.Models;

namespace Whitees.Interfaces;
public interface IShirtRepository
{
    Task<IEnumerable<Shirt>> GetAllShirts();
    Task<Shirt> GetShirtById(int id);
    Task<Shirt> GetShirtByIdNoTracking(int id);
    Task<bool> Add(Shirt shirt);
    Task<bool> Update(Shirt shirt);
    Task<bool> Delete(Shirt shirt);
    Task<bool> Save();

}