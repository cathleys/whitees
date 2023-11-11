namespace Whitees.Models;
public class Order
{

    public int Id { get; set; }

    public string Email { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public List<OrderItem> OrderItems { get; set; }
}
