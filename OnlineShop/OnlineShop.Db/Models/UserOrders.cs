using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class UserOrders
    {
        public Guid Id { get; set; }
       
        public Guid UserId { get; set; }
        
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
