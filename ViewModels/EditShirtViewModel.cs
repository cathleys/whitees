using System.ComponentModel.DataAnnotations;

namespace Whitees.ViewModels;
public class EditShirtViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Price is required")]
    public double Price { get; set; }

    [Required(ErrorMessage = "Price is required")]
    public IFormFile Image { get; set; }

    public string? ImageUrl { get; set; }
}
