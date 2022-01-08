
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class DeliveryInfoViewModel
    {
        [Required(ErrorMessage = "Не указано Имя")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "В имени должно быть не меньше 2 букв")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Не указан Адрес")]
        public string Adress { get; set; }


        [Required(ErrorMessage = "Не указан Телефон")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", 
            ErrorMessage = "Введите правильный номер телефона(через +7 XXX XX XX или 8 XXX XX XX) (не менее 7 цифр)")]
        public string PhoneNumber { get; set; }
    }
}
