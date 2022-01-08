using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Basket
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public List<BasketItem> Items { get; set; }

        public Basket()
        {
            Items = new List<BasketItem>();
        }
    }
}