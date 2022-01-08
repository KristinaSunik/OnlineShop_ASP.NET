using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class EditData
    {
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Введите валидный e-mail")]
        public string Login { get; set; }


        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$",
            ErrorMessage = "Введите правильный номер телефона(через +7 XXX XX XX или 8 XXX XX XX) (не менее 7 цифр)")]
        public string PhoneNumber { get; set; }

        
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Имя не может быть менее 2 букв")]
        public string FirstName { get; set; }

        public Guid Id { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Adress { get; set; }
    }
}
