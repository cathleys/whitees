using Whitees.Models;
using Whitees.Repositories;

namespace Whitees.ViewModels;
public class ShoppingCartVM
{
    public ShoppingCart ShoppingCart { get; set; }
    public double ShoppingCartTotal { get; set; }

    public List<ShoppingCartItem> CartItems { get; set; }
    public string AppUserId { get; set; }
}
