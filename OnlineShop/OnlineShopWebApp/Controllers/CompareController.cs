using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class CompareController : Controller
    {
        private readonly ICompareStorage compareStorage;
        private readonly IProductsStorage productsStorage;
        private readonly IUsersStorage usersStorage;
        private readonly IFavoritesStorage favoriteStorage;
        private readonly IBasketsStorage basketsStorage;

        public CompareController(IFavoritesStorage favoriteStorage, IBasketsStorage basketsStorage, ICompareStorage compareStorage, IUsersStorage usersStorage, IProductsStorage productsStorage)
        {
            this.compareStorage = compareStorage;
            this.productsStorage = productsStorage;
            this.usersStorage = usersStorage;
            this.favoriteStorage = favoriteStorage;
            this.basketsStorage = basketsStorage;
        }

        /// <summary> Shows Compares of current user, considering favorites and products in the basket </summary>
        public IActionResult Index()
        {
            var currentCompare = compareStorage.TryGetByUserId(Constants.UserId);
            var favoriteCurrentUser = favoriteStorage.TryGetByUserId(Constants.UserId);
            var itemsFromCurrentBasket = basketsStorage.TryGetByUserId(Constants.UserId);
            var currentComparesViewModel = currentCompare.ToCompareViewModel(favoriteCurrentUser, itemsFromCurrentBasket.Items);

            return View(currentComparesViewModel);
        }

        /// <summary> Adds product to Compares of current user </summary>
        public IActionResult Add(Guid productId)
        {
            var product = productsStorage.TryFindProductById(productId);

            compareStorage.Add(product, Constants.UserId);

            return RedirectToAction(nameof(Index));
        }

        /// <summary> Removes product from Compares of current user</summary>
        public IActionResult Remove(Guid productId)
        {
            compareStorage.Remove(productId, Constants.UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}

