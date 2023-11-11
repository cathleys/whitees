using Microsoft.EntityFrameworkCore;
using Whitees.Data;
using Whitees.Interfaces;
using Whitees.Models;

namespace Whitees.Repositories;
public class ShoppingCart : IShoppingCart
{
    private readonly DataContext _context;
    public string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }

    public ShoppingCart(DataContext context)
    {

        _context = context;
    }

    public ShoppingCart GetShoppingCart(string cartId)
    {
        return new ShoppingCart(_context) { ShoppingCartId = cartId };

    }
    public void AddCartOrderItem(Shirt shirt)
    {
        var shoppingCartItem = _context.ShoppingCartItems
        .FirstOrDefault(n => n.Shirt.Id == shirt.Id
                         && n.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem()
            {
                ShoppingCartId = ShoppingCartId,
                Shirt = shirt,
                Amount = 1
            };

            _context.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Amount++;
        }
        _context.SaveChanges();
    }

    public void RemoveCartOrderItem(Shirt shirt)
    {
        var shoppingCartItem = _context.ShoppingCartItems
        .FirstOrDefault(i => i.Shirt.Id == shirt.Id
        && i.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
            }
            else
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }

        }

        _context.SaveChangesAsync();
    }

    public async Task<List<ShoppingCartItem>> GetShoppingCartItems()
    {
        return ShoppingCartItems ??
        (ShoppingCartItems = await _context.ShoppingCartItems
        .Where(n => n.ShoppingCartId == ShoppingCartId)
        .Include(n => n.Shirt)
        .ToListAsync());
    }

    public double GetShoppingCartTotal()
    {
        return _context.ShoppingCartItems
        .Where(n => n.ShoppingCartId == ShoppingCartId)
        .Select(n => n.Shirt.Price * n.Amount)
        .Sum();
    }

    public async Task ClearShoppingCartAsync()
    {
        var items = await _context.ShoppingCartItems
        .Where(n => n.ShoppingCartId == ShoppingCartId)
        .ToListAsync();

        _context.ShoppingCartItems.RemoveRange(items);

        await _context.SaveChangesAsync();
    }
}
