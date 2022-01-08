using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Необходимо ввести имя продукта")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Имя товара не может быть менее 2 букв")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Необходимо ввести стоимость товара")]
        [DataType(DataType.Currency, ErrorMessage = "Стоимость товара не должна быть меньше 1")]
        public decimal Cost { get; set; }


        [Required(ErrorMessage = "Необходимо ввести подробное описание товара")]
        public string Description { get; set; }


        public string ImagePath { get; set; }

        public ProductCategory Category { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsInBasket { get; set; }
    }
}
