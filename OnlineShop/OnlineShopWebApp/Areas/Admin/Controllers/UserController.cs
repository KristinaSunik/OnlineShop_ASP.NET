using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Areas.Admin.Models;
using System;

namespace OnlineShopWebApp.Areas.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private readonly IUsersStorage usersStorage;

        public UserController(IUsersStorage usersStorage)
        {
            this.usersStorage = usersStorage;
        }

        /// <summary>All users (access for admin) to change</summary>
        public IActionResult Index()
        {
            return View(usersStorage.GetAll());
        }

        /// <summary>Exact user details (access for admin) to change</summary>
        public IActionResult Detail(Guid userId)
        {
            var user = usersStorage.TryFindByUserId(userId);

            return View(user);
        }

        /// <summary>Exact user (access for admin) to change password</summary>
        public IActionResult ChangePassword(Guid userId)
        {
            var user = usersStorage.TryFindByUserId(userId);

            var newPas = new NewPassword
            {
                Id = userId,
                Login = user.Login
            };

            return View(newPas);
        }

        [HttpPost]
        public IActionResult ChangePassword(NewPassword newPas)
        {
            if (newPas.Login == newPas.Password)
            {
                ModelState.AddModelError("", "Имя и пароль не должны совпадать");
            }

            if (ModelState.IsValid)
            {
                usersStorage.ChangePassword(newPas.Id, newPas.Password);
                
                return RedirectToAction(nameof(Detail), new { userId = newPas.Id});
            }

            return View(newPas);
        }

        /// <summary>Exact user (access for admin) to change details(data)</summary>
        public IActionResult Edit(Guid userId)
        {
            var user = usersStorage.TryFindByUserId(userId);

            var newData = new EditData
            {
                Id = userId,
                Login = user.Login,
                Password = user.Password
            };

            return View(newData);
        }

        [HttpPost]
        public IActionResult Edit(EditData newData)
        {
            if(newData.Login == newData.Password)
            {
                ModelState.AddModelError("", "Логин не должен совпадать с паролем");
            }

            if (ModelState.IsValid)
            {
                usersStorage.EditUserInfo(newData);

                return RedirectToAction(nameof(Detail), new { userId = newData.Id });
            }

            return View(newData);
        }

        /// <summary>Exact user(access for admin) to change rights</summary>
        public IActionResult ChangeRights(Guid userId)
        {
            var user = usersStorage.TryFindByUserId(userId);

            return View(user);
        }

        /// <summary>Exact user (access for admin) to delete</summary>
        public IActionResult Delete(Guid userId)
        {
            usersStorage.Remove(userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
