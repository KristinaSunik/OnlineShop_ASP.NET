using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db
{
    public interface ICompareStorage
    {
        /// <summary>Gets the entire list of Compares from Db </summary>
        List<Compare> GetAll { get; }

        /// <summary>Finds the comparesList of exact user</summary>
        Compare TryGetByUserId(Guid userId);

        /// <summary>
        /// Checks if there is such user's Comparelist - otherwise creates it,
        /// Checks if there is already such product in the List,
        /// if there is no such product in the Comparelist -> adds.
        /// </summary>
        void Add(Product product, Guid userId);

        /// <summary>
        /// Removes product from CompareList if there is such product in CompareList
        /// </summary>
        /// <param name="productId">id of the product to remove</param>
        void Remove(Guid productId, Guid userId);
    }
}