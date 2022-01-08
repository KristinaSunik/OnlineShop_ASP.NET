using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class OrdersDbStorage : IOrdersStorage
    {
        private readonly DatabaseContext databaseContext;

        public OrdersDbStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<UserOrders> GetAll { get { return databaseContext.UserOrders.Include(x => x.Orders)
                                                                                .ThenInclude(x => x.Items)
                                                                                .ThenInclude(x => x.Product)
                                                                                .ToList(); } }

        /// <summary>Gets all orders of exact user </summary>
        public UserOrders TryGetByUserId(Guid userId)
        {
            return databaseContext.UserOrders.Include(x => x.Orders)
                                         .ThenInclude(x => x.Items)
                                         .ThenInclude(x => x.Product)
                                         .FirstOrDefault(x => x.UserId == userId);
        }

        /// <summary>Gets the exact order of the exact user </summary>
        public Order TryGetByOrderId(Guid userId, Guid orderId)
        {
            return databaseContext.UserOrders.Include(x => x.Orders)
                                         .ThenInclude(x => x.Items)
                                         .ThenInclude(x => x.Product)
                                         .FirstOrDefault(x => x.UserId == userId).Orders
                                         .FirstOrDefault(x => x.Id == orderId);
        }

        /// <summary>
        /// Creates a new order with info from user and items from basket,
        /// adds it to user's orders
        /// </summary>
        /// <param name="deliverInfo"> Info about order(delivery) from user</param>
        /// <returns>created order Id</returns>
        public Guid AddNewOrderToUser(DeliveryInfo deliverInfo, Basket basket)
        {
            var userOrders = TryGetByUserId(basket.UserId);
            var currentOrder = new Order();

            if (userOrders != null)
            {
                currentOrder = new Order()
                {
                    Items = DeepClone(basket.Items),
                    Adress = deliverInfo.Adress,
                    PhoneNumber = deliverInfo.PhoneNumber,
                    Name = deliverInfo.Name
                };

                userOrders.Orders.Add(currentOrder);
            }
            else
            {
                currentOrder = new Order
                {
                    Items = DeepClone(basket.Items),
                    Name = deliverInfo.Name,
                    Adress = deliverInfo.Adress,
                    PhoneNumber = deliverInfo.PhoneNumber
                };

                databaseContext.Add(new UserOrders()
                {
                    UserId = basket.UserId,
                    Orders = new List<Order>() { currentOrder }
                });
            }

            databaseContext.SaveChanges();

            return currentOrder.Id;
        }

        /// <summary>Clones all items with its fields. Generate new object</summary>
        public List<BasketItem> DeepClone(List<BasketItem> items)
        {
            List<BasketItem> products = new List<BasketItem>();
            foreach (var item in items)
            {
                products.Add(new BasketItem()
                {
                    Amount = item.Amount,
                    Product = item.Product
                });

            }
            return products;
        }

        /// <summary> Saves new status of Order </summary>
        public void EditOrderStatus(Guid orderId, Guid userId, int newStatus)
        {
            var userOrder = TryGetByOrderId(userId, orderId);
            userOrder.Status = (OrderStatus)newStatus;

            databaseContext.SaveChanges();
        }
    }
}
