using Whitees.Data;

namespace Whitees.Models;
public class ShoppingCartItem
{
    public int Id { get; set; }

    public Shirt Shirt { get; set; }
    public int Amount { get; set; }

    public string ShoppingCartId { get; set; }
}
