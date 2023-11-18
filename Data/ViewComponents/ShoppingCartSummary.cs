using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Whitees.Extensions;
using Whitees.Repositories;

namespace Whitees.Data.ViewComponents;
public class ShoppingCartSummary : ViewComponent
{
    private readonly ShoppingCart _shoppingCart;

    public ShoppingCartSummary(
    ShoppingCart shoppingCart)
    {

        _shoppingCart = shoppingCart;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var items = await _shoppingCart.GetShoppingCartItems();

        return View(items.Count);
    }
}
