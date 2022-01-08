using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
