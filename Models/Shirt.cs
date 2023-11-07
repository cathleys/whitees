using System.ComponentModel.DataAnnotations;

namespace Whitees.Models;
public class Shirt
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    // public List<Photo> Photos { get; set; } = new List<Photo>();

}
