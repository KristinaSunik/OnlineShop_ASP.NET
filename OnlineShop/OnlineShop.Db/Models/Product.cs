using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Cost { get; set; }
        
        public string Description { get; set; }
        
        public string ImagePath { get; set; }
        
        public ProductCategory Category { get; set; }
        
        public List<BasketItem> BasketItems { get; set; }
        
        public Product()
        {
            BasketItems = new List<BasketItem>();
        }
    }
}

