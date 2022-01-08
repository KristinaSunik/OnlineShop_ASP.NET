using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class NewPassword
    {
        [Required(ErrorMessage = "Не указан Пароль")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Пароль должен быть не менее 4 символов")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Необходимо подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }


        public string Login { get; set; }

        public Guid Id { get; set; }
    }
}
