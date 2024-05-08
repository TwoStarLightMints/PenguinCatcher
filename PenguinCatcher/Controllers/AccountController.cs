using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PenguinCatcher.Models;
using PenguinCatcher.Models.IdentityModels;
using System.Diagnostics.Eventing.Reader;

namespace PenguinCatcher.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<PenguinCatcherUser> _userManager;
        private SignInManager<PenguinCatcherUser> _signInManager;

        public AccountController(UserManager<PenguinCatcherUser> userManager, SignInManager<PenguinCatcherUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = "")
        {
            var rvm = new RegisterViewModel { ReturnUrl = returnUrl };

            return View(rvm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                var user = new PenguinCatcherUser { UserName = rvm.UserName };
                
                var result = await _userManager.CreateAsync(user, rvm.Password);

                if (result.Succeeded)
                {
                    var res = await _userManager.AddToRoleAsync(user, "Catcher");

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(rvm);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);

                    if ((user != null) && await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        model.ReturnUrl = "/Admin/Dashboard/Index";
                    }
                    else if ((user != null) && await _userManager.IsInRoleAsync(user, "Catcher"))
                    {
                        model.ReturnUrl = "/User/Dashboard/Index";
                    }

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
