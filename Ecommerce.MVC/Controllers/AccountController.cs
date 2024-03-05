using Ecommerce.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(ReigisterUserDto Account) 
        {
            if (ModelState.IsValid) 
            {
                IdentityUser user = new IdentityUser();
                user.Email = Account.Email;
                user.UserName = Account.UserName;
                IdentityResult result = await _userManager.CreateAsync(user);
                if (result.Succeeded) 
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index","Book");
                }
                else
                {
                    foreach(var error  in result.Errors) 
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(Account);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel User)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.UserName);
                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult res = 
                        await _signInManager.PasswordSignInAsync(user,User.Password,User.RememberMe,false);
                    if (res.Succeeded) 
                    {
                        RedirectToAction("Index", "Book");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid UserName Or Password");
                }
            }

            return View(User);

        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
    }
}
