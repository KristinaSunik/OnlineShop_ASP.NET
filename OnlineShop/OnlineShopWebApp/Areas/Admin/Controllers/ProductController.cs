using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Areas.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IFavoritesStorage favoritesStorage;

        public ProductController(IProductsStorage productsStorage, IFavoritesStorage favoritesStorage)
        {
            this.productsStorage = productsStorage;
            this.favoritesStorage = favoritesStorage;
        }

        /// <summary>All products (access for admin) to change</summary>
        public IActionResult Index()
        {
            var productsViewModel = productsStorage.GetAll.ToProductsViewModel(new Favorite(), new List<BasketItem>());
            
            return View(productsViewModel);
        }

        /// <summary>Exact product (access for admin) to change</summary>
        public IActionResult Edit(Guid productId)
        {
            var product = productsStorage.TryFindProductById(productId);
            
            return View(product.ToProductViewModel(new Favorite(), new List<BasketItem>()));
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel product, Guid productId)
        {
            if (ModelState.IsValid)
            {
                if (product.Cost >= 1)
                {
                    var productToEdit = new Product()
                    {
                        Name = product.Name,
                        Cost = product.Cost,
                        Description = product.Description,
                        Id = productId,
                        ImagePath = product.ImagePath,
                        Category = (OnlineShop.Db.Models.ProductCategory)product.Category
                    };

                    productsStorage.EditProductInfo(productToEdit);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Стоимость товара не может быть меньше 1 рубля");

                    return View(product);
                }
            }
            return View(product);
        }

        /// <summary>Add new product (access for admin)</summary>
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.Cost >= 1)
                {
                    product.ImagePath = "/images/рыбка3.jpg";

                    var productDbToAdd = product.ToProduct();

                    productsStorage.Add(productDbToAdd);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Стоимость товара не может быть меньше 1 рубля");

                    return View(product);
                }
            }
            return View(product);
        }

        /// <summary>Remove product for admin</summary>
        public IActionResult Remove(Guid productId)
        {
            productsStorage.Remove(productId);

            return RedirectToAction(nameof(Index));
        }
    }
}
