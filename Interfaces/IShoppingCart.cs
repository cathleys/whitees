using Whitees.Models;

namespace Whitees.Interfaces;
public interface IShoppingCart
{
    void AddCartOrderItem(Shirt shirt);

    void RemoveCartOrderItem(Shirt shirt);
    Task<List<ShoppingCartItem>> GetShoppingCartItems();
    double GetShoppingCartTotal();
    Task ClearShoppingCartAsync();
}
