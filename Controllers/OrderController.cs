using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whitees.Interfaces;
using Whitees.Repositories;
using Whitees.ViewModels;

namespace Whitees.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(IUnitOfWork uow, ShoppingCart shoppingCart)
        {
            _uow = uow;
            _shoppingCart = shoppingCart;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _uow.OrderRepository.GetOrdersByUserIdAndRoleAsync();

            if (orders.Count <= 0)
            {
                TempData["Error"] = "You don't have orders yet. Make purchase now!";
            }
            return View(orders);
        }

        public async Task<IActionResult> ShoppingCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
                AppUserId = userId
            };

            return View(shoppingCartVM);
        }



        public async Task<IActionResult> AddItemToCart(int id)
        {
            var item = await _uow.ShirtRepository.GetShirtById(id);

            if (item != null)
            {
                _shoppingCart.AddCartOrderItem(item);
            }
            return RedirectToAction("ShoppingCart");
        }

        public async Task<IActionResult> RemoveItemFromCart(int id)
        {
            var item = await _uow.ShirtRepository.GetShirtById(id);

            if (item != null)
            {
                _shoppingCart.RemoveCartOrderItem(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _uow.OrderRepository.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}
