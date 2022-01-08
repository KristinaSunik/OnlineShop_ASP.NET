using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public interface IFavoritesStorage
    {
        /// <summary>Gets the entire list of Favorites from Db </summary>
        List<Favorite> GetAll { get; }

        /// <summary>Finds the favorites of exact user</summary>
        Favorite TryGetByUserId(Guid userId);

        /// <summary>
        /// Checks if there is such user's favorite list - otherwise creates it,
        /// Checks if there is already such product in the List -> removes from favorite,
        /// if there is no such product in the favorite list -> adds.</summary>
        void Add(Product product, Guid userId);

        /// <summary> Checks if the product in current user favoriteList.
        /// if it is - returns TRUE, otherwise returns FALSE</summary>
        /// <param name="productId">the product to check</param>
        /// <param name="favList">favoriteList of current user</param>
        static bool CheckIsFavorite(Guid productId, Favorite favList)
        {
            if (favList.Products.Contains(favList.Products.FirstOrDefault(x => x.Id == productId)))
            {
                return true;
            }

            return false;
        }
    }
}