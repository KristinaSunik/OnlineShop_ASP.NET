using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db
{
    public interface IOrdersStorage
    {
        /// <summary>Gets the entire list of Orders from Db </summary>
        List<UserOrders> GetAll { get; }

        /// <summary>Gets all orders of exact user </summary>
        UserOrders TryGetByUserId(Guid userId);

        /// <summary>Gets the exact order of the exact user </summary>
        Order TryGetByOrderId(Guid userId, Guid orderId);

        /// <summary>Clones all items with its fields. Generate new object</summary>
        List<BasketItem> DeepClone(List<BasketItem> items);

        /// <summary>
        /// Creates a new order with info from user and items from basket,
        /// adds it to user's orders
        /// </summary>
        /// <param name="deliverInfo"> Info about order(delivery) from user</param>
        /// <returns>created order Id</returns>
        Guid AddNewOrderToUser(DeliveryInfo deliverInfo, Basket existingBasket);
       
        /// <summary> Saves new status of Order </summary>
        void EditOrderStatus(Guid orderId, Guid userId, int newStatus);
    }
}