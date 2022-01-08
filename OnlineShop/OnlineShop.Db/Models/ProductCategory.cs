using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public enum ProductCategory
    {
        [Display(Name = "Неизвестно")]
        None,
        [Display(Name = "Домашние")]
        Domestic,
        [Display(Name = "Дикие")]
        Wild,
        [Display(Name = "Экзотические")]
        Exotic,
        [Display(Name = "Товары")]
        Goods
    };
}
