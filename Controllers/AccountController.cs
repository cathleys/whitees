using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Whitees.Data;
using Whitees.Models;
using Whitees.ViewModels;

namespace Whitees.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public AccountController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager
       )
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }


        public IActionResult Login()
        {
            var loginData = new LoginViewModel();

            return View(loginData);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lgVM)
        {
            if (!ModelState.IsValid) return View(lgVM);

            var user = await _userManager.FindByEmailAsync(lgVM.EmailAddress);

            if (user != null)
            {
                var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, lgVM.Password);

                if (isPasswordCorrect)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, lgVM.Password, false, false);

                    if (result.Succeeded) return RedirectToAction("Index", "Shirt");
                }
                TempData["Error"] = "Invalid password, Please try again";
                return View(lgVM);

            }
            TempData["Error"] = "User doesn't exist, please try again";
            return View(lgVM);
        }



        public IActionResult Register()
        {
            var register = new RegisterViewModel();

            return View(register);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rVM)
        {
            if (!ModelState.IsValid) return View(rVM);

            var user = await _userManager.FindByEmailAsync(rVM.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "Email is already taken, please try again.";
                return View(rVM);
            }

            var newUser = new AppUser
            {
                UserName = rVM.UserName,
                Email = rVM.EmailAddress,

            };

            var newUserResult = await _userManager.CreateAsync(newUser, rVM.Password);

            if (newUserResult.Succeeded)
            {

                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }


            return View("Login");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}