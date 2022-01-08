using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Adress { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public DateTime Date { get; set; }
        
        public OrderStatus Status { get; set; }
        
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        
        public Order()
        {
            Date = DateTime.Now;
            Status = OrderStatus.Created;
        }
    }
}
