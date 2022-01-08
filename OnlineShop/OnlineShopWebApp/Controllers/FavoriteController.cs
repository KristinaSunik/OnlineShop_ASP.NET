using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoritesStorage favoritesStorage;
        private readonly IProductsStorage productsStorage;
        private readonly IUsersStorage usersStorage;
        private readonly IBasketsStorage basketsStorage;

        public FavoriteController(IFavoritesStorage favoritesStorage, IBasketsStorage basketsStorage, IUsersStorage usersStorage, IProductsStorage productsStorage)
        {
            this.favoritesStorage = favoritesStorage;
            this.productsStorage = productsStorage;
            this.usersStorage = usersStorage;
            this.basketsStorage = basketsStorage;
        }

        /// <summary> Shows All Products from favorites of current user</summary>
        public IActionResult Index()
        {
            var currentFavorites = favoritesStorage.TryGetByUserId(Constants.UserId);
            var itemsFromCurrentBasket = basketsStorage.TryGetByUserId(Constants.UserId);
            var favoritesViewModel = currentFavorites.ToFavoriteViewModel(itemsFromCurrentBasket.Items);

            return View(favoritesViewModel);
        }

        /// <summary> Adds or Removes Product to/from favorites of current user</summary>
        public IActionResult Add(Guid productId)
        {
            var product = productsStorage.TryFindProductById(productId);
            favoritesStorage.Add(product, Constants.UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
