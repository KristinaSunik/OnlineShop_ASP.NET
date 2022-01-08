using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Linq;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IFavoritesStorage favoriteStorage;
        private readonly IBasketsStorage basketsStorage;

        public HomeController(IProductsStorage productsStorage, IBasketsStorage basketsStorage, IFavoritesStorage favoriteStorage)
        {
            this.productsStorage = productsStorage;
            this.favoriteStorage = favoriteStorage;
            this.basketsStorage = basketsStorage;
        }

        /// <summary> Shows All Products, 
        /// considering favorites of current user and filter</summary>
        public IActionResult Index(ProductCategory filter)
        {
            var products = from p in productsStorage.GetAll select p;

            if (filter != ProductCategory.None)
            {
                products = products.Where(s => (ProductCategory)s.Category == filter);
            }

            var favoriteCurrentUser = favoriteStorage.TryGetByUserId(Constants.UserId);
            var itemsFromCurrentBasket = basketsStorage.TryGetByUserId(Constants.UserId);
            var productsViewModel = products.ToList().ToProductsViewModel(favoriteCurrentUser, itemsFromCurrentBasket.Items);

            return View(productsViewModel);
        }

        /// <summary> Shows All Products, 
        /// considering favorites of current user and categories</summary>
        [HttpPost]
        public IActionResult Index(string filter)
        {
            var products = from p in productsStorage.GetAll
                           select p;

            if (!String.IsNullOrEmpty(filter))
            {
                products = products.Where(s => s.Name.ToLower().Contains(filter.ToLower()) ||
                                               EnumHelper.GetDisplayName(s.Category).ToLower().Contains(filter.ToLower()));
            }

            return View(products.ToList());
        }
    }
}
