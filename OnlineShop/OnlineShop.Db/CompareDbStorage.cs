using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class CompareDbStorage : ICompareStorage
    {
        private readonly DatabaseContext databaseContext;

        public CompareDbStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<Compare> GetAll { get { return databaseContext.Compares.ToList(); } }

        /// <summary>Finds the comparesList of exact user</summary>
        public Compare TryGetByUserId(Guid userId)
        {
            var curentCompare = databaseContext.Compares.Include(x => x.Products)
                                                        .FirstOrDefault(x => x.UserId == userId);

            return curentCompare;
        }

        /// <summary>
        /// Checks if there is such user's Comparelist - otherwise creates it,
        /// Checks if there is already such product in the List,
        /// if there is no such product in the Comparelist -> adds.
        /// </summary>
        public void Add(Product product, Guid userId)
        {
            var existingCompareList = TryGetByUserId(userId);

            if (existingCompareList != null)
            {
                existingCompareList.Products.Add(product);
            }
            else
            {
                databaseContext.Compares.Add(new Compare()
                {
                    UserId = userId,
                    Products = new List<Product> { product }
                });
            }

            databaseContext.SaveChanges();
        }

        /// <summary>
        /// Removes product from CompareList if there is such product in CompareList
        /// </summary>
        /// <param name="productId">id of the product to remove</param>
        public void Remove(Guid productId, Guid userId)
        {
            var currentCompareList = TryGetByUserId(userId);

            if (currentCompareList != null)
            {
                currentCompareList.Products.Remove(currentCompareList.Products
                                           .FirstOrDefault(x => x.Id == productId));
            }

            databaseContext.SaveChanges();
        }
    }
}
