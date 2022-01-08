using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public interface IBasketsStorage
    {
        /// <summary>Gets the entire list of baskets from Db </summary>
        List<Basket> GetAll { get; }

        /// <summary> Finds basket of exact user</summary>
        Basket TryGetByUserId(Guid userId);

        /// <summary>
        /// Checks if there is such basket - otherwise creates it,
        /// Checks if there is already such product in the basket -> quantity++,
        /// if there is no such product in the basket -> quantity = 1.
        /// </summary>
        void Add(Product product, Guid userId);

        /// <summary>
        /// Checks if there is such product in the basket -> quantity--,
        /// if quantity = 0, removes item from current basket.
        /// </summary>
        /// <param name="productId">Id of the product where is needed to decrease amount</param>
        /// <param name="userId">from whoose basket is the product</param>
        void RemoveProduct(Guid productId, Guid userId);

        /// <summary>
        /// Checks if there is such product in the basket,
        /// removes completely item from current basket.
        /// </summary>
        /// <param name="productId">Id of the product where is needed to delete product</param>
        /// <param name="userId">from which basket is the product</param>
        void DeleteProduct(Guid productId, Guid userId);

        /// <summary>
        /// Cleares all products from basket. 
        /// Usually uses after creating an order
        /// </summary>
        void DeleteAllProducts(Guid userId);

        /// <summary> Checks if the product is in current user Basket.
        /// if it is - returns TRUE, otherwise returns FALSE</summary>
        /// <param name="productId">the product to check</param>
        /// <param name="favList">favoriteList of current user</param>
        static bool CheckIsInBasket(Guid productId, List<BasketItem> itemsFromBasketCurrentUser)
        {
            if (itemsFromBasketCurrentUser.Contains(itemsFromBasketCurrentUser.FirstOrDefault(x => x.Product.Id == productId)))
            {
                return true;
            }

            return false;
        }
    }
}