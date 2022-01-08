using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IBasketsStorage basketsStorage;
        private readonly IUsersStorage usersStorage;
        private readonly IFavoritesStorage favoritesStorage;

        public BasketController(IProductsStorage productsStorage, IFavoritesStorage favoritesStorage, IUsersStorage usersStorage, IBasketsStorage basketsStorage)
        {
            this.productsStorage = productsStorage;
            this.basketsStorage = basketsStorage;
            this.usersStorage = usersStorage;
            this.favoritesStorage = favoritesStorage;
        }

        /// <summary> Shows basket of current user </summary>
        public IActionResult Index()
        {
            var basket = basketsStorage.TryGetByUserId(Constants.UserId);
            var favorite = favoritesStorage.TryGetByUserId(Constants.UserId);
            var basketViewModel = basket.ToBasketViewModel(favorite);

            return View(basketViewModel);
        }

        /// <summary> Adds product to basket or encreases amount of the product</summary>
        public IActionResult Add(Guid productId)
        {
            var product = productsStorage.TryFindProductById(productId);

            if (product != null)
            {
                basketsStorage.Add(product, Constants.UserId);
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>Decreases the amount of the product from basket </summary>
        public IActionResult Remove(Guid productId)
        {
            if (productsStorage.TryFindProductById(productId) != null)
            {
                basketsStorage.RemoveProduct(productId, Constants.UserId);
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary> Totaly removes the product from basket </summary>
        public IActionResult Delete(Guid productId)
        {
            if (productsStorage.TryFindProductById(productId) != null)
            {
                basketsStorage.DeleteProduct(productId, Constants.UserId);
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary> Deletes ALl products from the basket </summary>
        public IActionResult Clear()
        {
            basketsStorage.DeleteAllProducts(Constants.UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
