using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static List<ProductViewModel> ToProductsViewModel(this List<Product> products, Favorite favoriteCurrentUser, List<BasketItem> itemsFromBasketCurrentUser)
        {
            if (products == null)
            {
                return null;
            }

            var productsViewModel = new List<ProductViewModel>();

            foreach (var product in products)
            {
                productsViewModel.Add(product.ToProductViewModel(favoriteCurrentUser, itemsFromBasketCurrentUser));
            }
            return productsViewModel;
        }

        public static List<UserOrdersViewModel> ToUserOrdersViewModel(this List<UserOrders> listUserOrders, Favorite favoriteCurrentUser)
        {
            var listUserOrdersViewModel = new List<UserOrdersViewModel>();

            foreach (var userOrders in listUserOrders)
            {
                listUserOrdersViewModel.Add(userOrders.ToUserOrdersViewModel(favoriteCurrentUser));
            }

            return listUserOrdersViewModel;
        }

        public static UserOrdersViewModel ToUserOrdersViewModel(this UserOrders userOrders, Favorite favoriteCurrentUser)
        {
            if (userOrders == null)
            {
                return null;
            }

            var userOrdersViewModel = new UserOrdersViewModel()
            {
                UserId = userOrders.UserId
            };

            foreach (var order in userOrders.Orders)
            {
                var currentOrder = order.ToOrderViewModel(favoriteCurrentUser);

                userOrdersViewModel.Orders.Add(currentOrder);
            }

            return userOrdersViewModel;
        }

        public static ProductViewModel ToProductViewModel(this Product product, Favorite favoriteCurrentUser, List<BasketItem> itemsFromBasketCurrentUser)
        {
            if (product == null)
            {
                return null;
            }

            if (favoriteCurrentUser == null)
            {
                favoriteCurrentUser = new Favorite();
            }

            return new ProductViewModel()
            {
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                Id = product.Id,
                ImagePath = product.ImagePath,
                IsFavorite = IFavoritesStorage.CheckIsFavorite(product.Id, favoriteCurrentUser),
                IsInBasket = IBasketsStorage.CheckIsInBasket(product.Id, itemsFromBasketCurrentUser),
                Category = (Models.ProductCategory)product.Category
            };
        }

        public static DeliveryInfo ToDeliveryInfo(this DeliveryInfoViewModel deliverInfoViewModel)
        {
            return new DeliveryInfo()
            {
                Name = deliverInfoViewModel.Name,
                Adress = deliverInfoViewModel.Adress,
                PhoneNumber = deliverInfoViewModel.PhoneNumber
            };
        }

        public static OrderViewModel ToOrderViewModel(this Order currentOrder, Favorite favoriteCurrentUser)
        {
            if (currentOrder == null)
            {
                return null;
            }

            return new OrderViewModel()
            {
                Id = currentOrder.Id,
                Items = currentOrder.Items.ToBasketItemsViewModel(favoriteCurrentUser),
                Name = currentOrder.Name,
                Adress = currentOrder.Adress,
                PhoneNumber = currentOrder.PhoneNumber,
                Status = (Areas.Admin.Models.OrderStatus)currentOrder.Status,
                Date = currentOrder.Date
            };
        }

        public static CompareViewModel ToCompareViewModel(this Compare compare, Favorite favoriteCurrentUser, List<BasketItem> itemsFromBasketCurrentUser)
        {
            if (compare == null)
            {
                return null;
            }

            return new CompareViewModel()
            {
                Id = compare.Id,
                UserId = compare.UserId,
                Items = compare.Products.ToProductsViewModel(favoriteCurrentUser, itemsFromBasketCurrentUser)
            };
        }

        public static Product ToProduct(this ProductViewModel product)
        {
            return new Product()
            {
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                Id = product.Id,
                ImagePath = product.ImagePath,
                Category = (OnlineShop.Db.Models.ProductCategory)product.Category
            };
        }

        public static FavoriteViewModel ToFavoriteViewModel(this Favorite favorite, List<BasketItem> itemsFromBasketCurrentUser)
        {
            if (favorite == null)
            {
                return new FavoriteViewModel();
            }

            return new FavoriteViewModel()
            {
                Id = favorite.Id,
                UserId = favorite.UserId,
                Items = favorite.Products.ToProductsViewModel(favorite, itemsFromBasketCurrentUser)
            };
        }

        public static BasketViewModel ToBasketViewModel(this Basket basket, Favorite favoriteCurrentUser)
        {
            if (basket == null)
            {
                return null;
            }

            return new BasketViewModel()
            {
                Id = basket.Id,
                UserId = basket.UserId,
                Items = basket.Items.ToBasketItemsViewModel(favoriteCurrentUser)
            };
        }


        public static List<BasketItemViewModel> ToBasketItemsViewModel(this List<BasketItem> items, Favorite favoriteCurrentUser)
        {
            var products = new List<BasketItemViewModel>();
            foreach (var item in items)
            {
                products.Add(new BasketItemViewModel()
                {
                    Amount = item.Amount,
                    Product = new ProductViewModel()
                    {
                        Name = item.Product.Name,
                        Cost = item.Product.Cost,
                        Description = item.Product.Description,
                        Id = item.Product.Id,
                        IsFavorite = IFavoritesStorage.CheckIsFavorite(item.Product.Id, favoriteCurrentUser),
                        IsInBasket = true,
                        ImagePath = item.Product.ImagePath,
                        Category = (Models.ProductCategory)item.Product.Category
                    }
                }); ;
            }

            return products;
        }
    }
}
