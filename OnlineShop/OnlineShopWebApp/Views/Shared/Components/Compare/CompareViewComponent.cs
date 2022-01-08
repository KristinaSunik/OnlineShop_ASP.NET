using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;

namespace OnlineShopWebApp.Views.Shared.Components.Compare
{
    public class CompareViewComponent : ViewComponent
    {
        private readonly ICompareStorage comparesRepository;

        public CompareViewComponent(ICompareStorage comparesRepository)
        {
            this.comparesRepository = comparesRepository;
        }

        /// <summary> Shows amount of items in compare at header </summary>
        public IViewComponentResult Invoke()
        {
            var compares = comparesRepository.TryGetByUserId(Constants.UserId);

            var itemsAmount = compares?.Products.Count ?? 0;

            return View("Compare", itemsAmount);
        }

        
    }
}
