using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IFavoritesStorage favoritesStorage;
        private readonly IBasketsStorage basketsStorage;

        public ProductController(IProductsStorage productsStorage, IBasketsStorage basketsStorage, IFavoritesStorage favoritesStorage)
        {
            this.productsStorage = productsStorage;
            this.favoritesStorage = favoritesStorage;
            this.basketsStorage = basketsStorage;
        }

        /// <summary> Shows Exact Product, 
        /// considering favorites of current user</summary>
        public IActionResult Index(Guid productId)
        {
            var product = productsStorage.TryFindProductById(productId);
            var currentFavorites = favoritesStorage.TryGetByUserId(Constants.UserId);
            var itemsFromCurrentBasket = basketsStorage.TryGetByUserId(Constants.UserId);
            var productVewModel = product.ToProductViewModel(currentFavorites, itemsFromCurrentBasket.Items);

            return View(productVewModel);
        }
    }
}
