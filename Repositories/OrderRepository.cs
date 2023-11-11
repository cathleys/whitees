using Microsoft.EntityFrameworkCore;
using Whitees.Data;
using Whitees.Extensions;
using Whitees.Interfaces;
using Whitees.Models;

namespace Whitees.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync()
    {
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var userRole = _httpContextAccessor.HttpContext.User.GetUserRole();

        var orders = await _context.Orders
        .Include(o => o.OrderItems)
        .ThenInclude(o => o.Shirt)
        .Include(s => s.AppUser)
        .ToListAsync();

        if (userRole != UserRoles.Admin)
        {
            orders = orders.Where(o => o.AppUserId == userId).ToList();
        }

        return orders;
    }
    public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
    {
        var order = new Order
        {
            AppUserId = userId,
            Email = userEmailAddress,
        };

        await _context.AddAsync(order);
        await _context.SaveChangesAsync();


        foreach (var item in items)
        {
            var orderItem = new OrderItem
            {
                Amount = item.Amount,
                ShirtId = item.Shirt.Id,
                OrderId = order.Id,
                Price = item.Shirt.Price
            };

            await _context.OrderItems.AddAsync(orderItem);

        }
        await _context.SaveChangesAsync();
    }



}
