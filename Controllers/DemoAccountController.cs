using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Whitees.Models;
using Whitees.ViewModels;

namespace Whitees.Controllers
{
    public class DemoAccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public DemoAccountController(UserManager<AppUser> userManager,
         SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> DemoAdmin(DemoAdminViewModel demoVM)
        {
            var user = await _userManager.FindByEmailAsync(demoVM.EmailAddress);

            if (user != null)
            {
                var password = await _userManager.CheckPasswordAsync(user, demoVM.Password);
                if (password)
                {

                    var result = await _signInManager.PasswordSignInAsync(user, demoVM.Password, false, false);
                    if (result.Succeeded) return RedirectToAction("Index", "Shirt");
                }
                TempData["Error"] = "Invalid password, Please try again";
                return View(demoVM);

            }
            TempData["Error"] = "User doesn't exist, please try again";
            return View(demoVM);
        }

        public async Task<IActionResult> DemoUser(DemoUserViewModel demoVM)
        {
            var user = await _userManager.FindByEmailAsync(demoVM.EmailAddress);

            if (user != null)
            {
                var password = await _userManager.CheckPasswordAsync(user, demoVM.Password);
                if (password)
                {

                    var result = await _signInManager.PasswordSignInAsync(user, demoVM.Password, false, false);
                    if (result.Succeeded) return RedirectToAction("Index", "Shirt");
                }
                TempData["Error"] = "Invalid password, Please try again";
                return View(demoVM);

            }
            TempData["Error"] = "User doesn't exist, please try again";
            return View(demoVM);
        }
    }
}
