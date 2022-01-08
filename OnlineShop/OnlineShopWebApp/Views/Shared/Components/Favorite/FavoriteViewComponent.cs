using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;

namespace OnlineShopWebApp.Views.Shared.Components.Favorite
{
    public class FavoriteViewComponent : ViewComponent
    {
        private readonly IFavoritesStorage favoritesRepository;

        public FavoriteViewComponent(IFavoritesStorage favoritesRepository)
        {
            this.favoritesRepository = favoritesRepository;
        }

        /// <summary> Shows amount of items in favorites at header </summary>
        public IViewComponentResult Invoke()
        {
            var favorites = favoritesRepository.TryGetByUserId(Constants.UserId);

            var itemsAmount = favorites?.Products.Count ?? 0;

            return View("Favorite", itemsAmount);
        }

        
    }
}
