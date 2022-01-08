using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class FavoriteViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public List<ProductViewModel> Items { get; set; } = new List<ProductViewModel>();
    }
}
