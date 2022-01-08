using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;

namespace OnlineShopWebApp.Areas.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]

    public class HomeController : Controller
    {
        /// <summary>Admin left panel to work with\change data (access for admin)</summary>
        public IActionResult LeftPanel()
        {
            return View();
        }
    }
}
