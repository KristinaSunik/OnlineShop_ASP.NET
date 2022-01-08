using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Не указан Логин")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Введите валидный e-mail")]
        public string Login { get; set; }


        [Required(ErrorMessage = "Не указан Пароль")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Пароль должен быть не менее 4 символов")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Пароль не подтвержден")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
