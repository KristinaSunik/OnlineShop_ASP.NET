using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System;
using OrderStatus = OnlineShopWebApp.Areas.Admin.Models.OrderStatus;

namespace OnlineShopWebApp.Areas.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrdersStorage ordersStorage;

        public OrderController(IOrdersStorage ordersStorage)
        {
            this.ordersStorage = ordersStorage;
        }

        /// <summary> All existing Orders from Db (access for admin)</summary>
        public IActionResult Index()
        {
            var orders = ordersStorage.GetAll;
            var userOrdersViewModel = orders.ToUserOrdersViewModel(new Favorite());

            return View(userOrdersViewModel);

        }

        /// <summary> Exact order to change status (access for admin)</summary>
        public IActionResult Edit(Guid orderId)
        {
            var currentOrder = ordersStorage.TryGetByOrderId(Constants.UserId, orderId);
            var currentOrderViewModel = currentOrder.ToOrderViewModel(new Favorite());

            return View(currentOrderViewModel);
        }

        [HttpPost]
        public IActionResult Edit(Guid orderId, OrderStatus status)
        {
            ordersStorage.EditOrderStatus(orderId, Constants.UserId, Convert.ToInt32(status));

            return RedirectToAction(nameof(Index));
        }
    }
}
