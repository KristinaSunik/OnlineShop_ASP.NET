using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.Components.Basket
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IBasketsStorage basketsStorage;
        private readonly IFavoritesStorage favoritesStorage;

        public BasketViewComponent(IBasketsStorage basketsStorage, IFavoritesStorage favoritesStorage)
        {
            this.basketsStorage = basketsStorage;
            this.favoritesStorage = favoritesStorage;
        }

        /// <summary> Shows amount of items in basket at header </summary>
        public IViewComponentResult Invoke()
        {
            var basket = basketsStorage.TryGetByUserId(Constants.UserId);
            var favorite = favoritesStorage.TryGetByUserId(Constants.UserId);
            var basketViewModel = basket.ToBasketViewModel(favorite);

            var itemsAmount = basketViewModel?.ItemsAmount ?? 0;

            return View("Basket", itemsAmount);
        }

        
    }
}
