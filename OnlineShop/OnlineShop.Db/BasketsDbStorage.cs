using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class BasketsDbStorage : IBasketsStorage
    {
        private readonly DatabaseContext databaseContext;

        public BasketsDbStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<Basket> GetAll { get { return databaseContext.Baskets.ToList(); } }

        /// <summary> Finds basket of exact user</summary>
        public Basket TryGetByUserId(Guid userId)
        {
            return databaseContext.Baskets.Include(x => x.Items)
                                          .ThenInclude(x => x.Product)
                                          .FirstOrDefault(x => x.UserId == userId);
        }

        /// <summary>
        /// Checks if there is such basket - otherwise creates it,
        /// Checks if there is already such product in the basket -> quantity++,
        /// if there is no such product in the basket -> quantity = 1.
        /// </summary>
        public void Add(Product product, Guid userId)
        {
            if (TryGetByUserId(userId) is Basket existingBasket)
            {
                var excistingBasketItem = existingBasket.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (excistingBasketItem != null)
                {
                    excistingBasketItem.Amount += 1;
                }
                else
                {
                    existingBasket.Items.Add(new BasketItem
                    {
                        Amount = 1,
                        Product = product,
                        Basket = existingBasket
                    });
                }
            }
            else
            {
                var newBasket = new Basket()
                {
                    UserId = userId,
                };

                newBasket.Items = new List<BasketItem>()
                {
                    new BasketItem()
                    {
                        Amount = 1,
                        Product = product,
                        Basket = newBasket
                    }
                };

                databaseContext.Baskets.Add(newBasket);
            }

            databaseContext.SaveChanges();
        }

        /// <summary>
        /// Checks if there is such product in the basket -> quantity--,
        /// if quantity = 0, removes item from current basket.
        /// </summary>
        /// <param name="productId">where is needed to decrease amount</param>
        /// <param name="userId">from which basket is the product</param>
        public void RemoveProduct(Guid productId, Guid userId)
        {
            var currentBasket = TryGetByUserId(userId);

            var excistingBasketItem = currentBasket.Items.FirstOrDefault(x => x.Product.Id == productId);
            if (excistingBasketItem != null && excistingBasketItem.Amount != 0)
            {
                excistingBasketItem.Amount -= 1;
            }

            databaseContext.SaveChanges();
        }

        /// <summary>
        /// Checks if there is such product in the basket,
        /// removes item from current basket.
        /// </summary>
        /// <param name="productId">where is needed to delete product</param>
        /// <param name="userId">from which basket is the product</param>
        public void DeleteProduct(Guid productId, Guid userId)
        {
            var currentBasket = TryGetByUserId(userId);

            var excistingBasketItem = currentBasket.Items.FirstOrDefault(x => x.Product.Id == productId);
            if (excistingBasketItem != null)
            {
                currentBasket.Items.Remove(excistingBasketItem);
            }

            databaseContext.SaveChanges();
        }

        /// <summary>
        /// Cleares all products from basket. 
        /// Usually uses after creating an order
        /// </summary>
        public void DeleteAllProducts(Guid userId)
        {
            var currentBasket = TryGetByUserId(userId);

            if (currentBasket != null)
            {
                currentBasket.Items.Clear();
            }

            databaseContext.SaveChanges();
        }
    }
}
