using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersStorage ordersStorage;
        private readonly IBasketsStorage basketsStorage;
        private readonly IUsersStorage usersStorage;

        public OrderController(IOrdersStorage ordersRepository, IUsersStorage usersStorage, IBasketsStorage basketsRepository)
        {
            this.ordersStorage = ordersRepository;
            this.basketsStorage = basketsRepository;
            this.usersStorage = usersStorage;
        }

        [Authorize]
        /// <summary>Gets Data from user about order(delivery)</summary>
        public IActionResult CheckOut()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CheckOut(DeliveryInfoViewModel deliverInfoViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingBasket = basketsStorage.TryGetByUserId(Constants.UserId);
                var deliverInfo = deliverInfoViewModel.ToDeliveryInfo();
                var orderId = ordersStorage.AddNewOrderToUser(deliverInfo, existingBasket);

                basketsStorage.DeleteAllProducts(Constants.UserId);

                return RedirectToAction(nameof(Index), new { orderId = orderId });
            }
            return View(deliverInfoViewModel);
        }

        /// <summary> Shows All user's orders</summary>
        public IActionResult Show()
        {
            var orders = ordersStorage.TryGetByUserId(Constants.UserId);
            var userOrdersViewModel = orders.ToUserOrdersViewModel(new Favorite());

            return View(userOrdersViewModel);
        }

        [Authorize]
        /// <summary> Shows All data of just created order</summary>
        public IActionResult Index(Guid orderId)
        {
            var currentOrder = ordersStorage.TryGetByOrderId(Constants.UserId, orderId);
            var currentOrderViewModel = currentOrder.ToOrderViewModel(new Favorite());

            return View(currentOrderViewModel);
        }
    }
}
