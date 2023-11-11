using Whitees.Models;

namespace Whitees.Interfaces;
public interface IOrderRepository
{
    Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
    Task<List<Order>> GetOrdersByUserIdAndRoleAsync();

}
