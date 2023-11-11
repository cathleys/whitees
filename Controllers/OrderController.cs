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
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCart _shoppingCart;
        private readonly IShirtRepository _shirtRepository;

        public OrderController(IOrderRepository orderRepository,
        IShoppingCart shoppingCart,
        IShirtRepository shirtRepository
         )
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _shirtRepository = shirtRepository;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.GetOrdersByUserIdAndRoleAsync();

            if (orders.Count <= 0)
            {
                TempData["Error"] = "You don't have orders yet. Make purchase now!";
            }
            return View(orders);
        }

        public async Task<IActionResult> ShoppingCart()
        {

            var shoppingCartVM = new ShoppingCartVM
            {
                ShoppingCart = (ShoppingCart)_shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
                CartItems = await _shoppingCart.GetShoppingCartItems()
            };

            return View(shoppingCartVM);
        }


        public async Task<IActionResult> AddItemToCart(int id)
        {
            var item = await _shirtRepository.GetShirtById(id);

            if (item != null)
            {
                _shoppingCart.AddCartOrderItem(item);
            }
            return RedirectToAction("ShoppingCart");
        }

        public async Task<IActionResult> RemoveItemFromCart(int id)
        {
            var item = await _shirtRepository.GetShirtById(id);

            if (item != null)
            {
                _shoppingCart.RemoveCartOrderItem(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _orderRepository.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}
