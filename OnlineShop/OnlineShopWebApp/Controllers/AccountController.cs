using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersStorage usersManager;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(IUsersStorage usersManager, SignInManager<User> _signInManager, UserManager<User> _userManager)
        {
            this.usersManager = usersManager;
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }

        /// <summary> Enter the account </summary>
        public IActionResult Login(string returnURL)
        {
            return View(new Login() { ReturnURL = returnURL});
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = signInManager.PasswordSignInAsync(login.UserName, login.Password, login.Remember, false).Result;

                if (result.Succeeded)
                {
                    if (login.ReturnURL != null)
                    {
                        return Redirect(login.ReturnURL);
                    }
                    else
                    {
                        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
                    }
                }
                else
                {

                    ModelState.AddModelError("", "Логин или пароль указаны неверно");
                }
            }
            return View(login);
        }

        /// <summary> Gets data from user to register</summary>
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register registration)
        {
            if (registration.Login == registration.Password)
            {
                ModelState.AddModelError("", "Имя и пароль не должны совпадать");
            }

            if (ModelState.IsValid)
            {
                var newUser = new User()
                {
                    PasswordHash = registration.Password,
                    UserName = registration.Login,
                    Email = registration.Login,
                };

                var result = userManager.CreateAsync(newUser, registration.Password).Result;

                if (result.Succeeded)
                {
                    signInManager.SignInAsync(newUser, false);

                    return RedirectToAction(nameof(AccountController.Login), nameof(AccountController).Replace("Controller", ""));
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(registration);
        }

        public IActionResult Logout()
        {
            signInManager.SignOutAsync().Wait();

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
