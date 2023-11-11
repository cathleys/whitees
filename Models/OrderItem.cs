namespace Whitees.Models;
public class OrderItem
{
    public int Id { get; set; }

    public int Amount { get; set; }
    public double Price { get; set; }

    public int ShirtId { get; set; }
    public Shirt Shirt { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}
