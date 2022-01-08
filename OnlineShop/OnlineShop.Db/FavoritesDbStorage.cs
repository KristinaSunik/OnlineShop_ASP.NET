using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public class FavoritesDbStorage : IFavoritesStorage
    {
        private readonly DatabaseContext databaseContext;

        public FavoritesDbStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<Favorite> GetAll { get { return databaseContext.Favorites.ToList(); } }

        /// <summary>Finds the favorites of exact user</summary>
        public Favorite TryGetByUserId(Guid userId)
        {
            var curentFavorite = databaseContext.Favorites.Include(x => x.Products)
                                                          .FirstOrDefault(Favorite => Favorite.UserId == userId);
            return curentFavorite;
        }

        /// <summary> Goes through favoritesList to find Product with exact id </summary>
        public Product TryFindProductById(Guid productId, Favorite favList)
        {
            return favList.Products.FirstOrDefault(x => x.Id == productId);
        }

        /// <summary>
        /// Checks if there is such user's favorite list,
        /// Checks if there is already such product in the List -> removes from favorite,
        /// if there is no such product in the favorite list -> adds.
        /// </summary>
        public void Add(Product product, Guid userId)
        {
            var existingFavoriteList = TryGetByUserId(userId);

            if (existingFavoriteList != null)
            {
                AddOrRemove(product, existingFavoriteList);
            }
            else
            {
                databaseContext.Favorites.Add( new Favorite()
                {
                    UserId = userId,
                    Products = new List<Product> { product }
                });
            }

            databaseContext.SaveChanges();
        }

        /// <summary>
        /// Adds product to favorites of User if there is no such product in favoritesList
        /// Adds users Login To product UsersMarkAsFav
        /// otherwise - removes it from the list
        /// </summary>
        /// <param name="id">id of the product to add</param>
        public void AddOrRemove(Product product, Favorite favList)
        {
            var productFromFavList = TryFindProductById(product.Id, favList);

            if (productFromFavList == null)
            {
                favList.Products.Add(product);
            }
            else
            {
                favList.Products.Remove(product);
            }
        }
    }
}

