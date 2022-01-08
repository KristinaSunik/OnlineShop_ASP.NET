using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class UserOrdersViewModel
    {
        public Guid UserId { get; set; }

        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();

    }
}
