using System.ComponentModel.DataAnnotations;

namespace Whitees.Models;
public class Shirt
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }


}
