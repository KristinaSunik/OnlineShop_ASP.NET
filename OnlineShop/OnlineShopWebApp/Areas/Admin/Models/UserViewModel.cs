using System;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Adress { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public bool Remember { get; set; }

        public UserViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
