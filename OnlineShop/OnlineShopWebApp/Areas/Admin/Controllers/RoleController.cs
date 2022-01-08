using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Areas.Admin.Models;

namespace OnlineShopWebApp.Areas.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RoleController : Controller
    {
        private readonly IRolesStorage rolesStorage;

        public RoleController(IRolesStorage rolesStorage)
        {
            this.rolesStorage = rolesStorage;
        }

        /// <summary>All roles (access for admin) to change</summary>
        public IActionResult Index()
        {
            var roles = rolesStorage.GetAll();

            return View(roles);
        }

        /// <summary>Totaly remove role (access for admin)</summary>
        public IActionResult Remove(string roleName)
        {
            rolesStorage.Remove(roleName);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>Add new role (access for admin)</summary>
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Role newRole)
        {
            if (rolesStorage.TryFindRoleByName(newRole.Name) != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует");

                return View(newRole);
            }
           
            if(ModelState.IsValid)
            {
                rolesStorage.Add(newRole);

                return RedirectToAction(nameof(Index));
            }
            return View(newRole);
        }
    }
}
