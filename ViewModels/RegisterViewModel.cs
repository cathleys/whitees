using System.ComponentModel.DataAnnotations;

namespace Whitees.ViewModels;
public class RegisterViewModel
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; }

    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email address is required")]
    public string EmailAddress { get; set; }


    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password do not match")]
    public string ConfirmPassword { get; set; }

}
