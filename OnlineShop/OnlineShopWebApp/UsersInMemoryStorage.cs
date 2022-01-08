using OnlineShopWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public class UsersInMemoryStorage : IUsersStorage
    {
        private readonly List<UserViewModel> users = new List<UserViewModel>();

        public List<UserViewModel> GetAll()
        { 
            return users; 
        }

        public UserViewModel TryGetByUserLogin(string userLogin)
        {
            return users.FirstOrDefault(x => x.Login == userLogin);
        }

       public UserViewModel TryFindByUserId(Guid userId)
        {
            return users.FirstOrDefault(x => x.Id == userId);
        }

        /// <summary>
        /// Checks if there is user with such Login,
        /// if there is no such user in the list -> Adds.
        /// </summary>
        public bool Add(UserViewModel user)
        {
            var currentUser = TryGetByUserLogin(user.Login);
            if (currentUser == null)
            {
                users.Add(user);
                return true;
            }
            else
            {
                //юзер с таким логином существует
                return false;
            }
        }

        public void ChangePassword(Guid userId, string newPassword)
        {
            var user = TryFindByUserId(userId);

            user.Password = newPassword;
            user.PasswordConfirm = newPassword;
        }

        public void EditUserInfo(EditData newData)
        {
            var userToChange = TryFindByUserId(newData.Id);

            userToChange.Login = newData?.Login ?? userToChange.Login;
            userToChange.FirstName = newData?.FirstName ?? userToChange.FirstName;
            userToChange.LastName = newData?.LastName ?? userToChange.LastName;
            userToChange.Adress = newData?.Adress ?? userToChange.Adress;
            userToChange.PhoneNumber = newData?.PhoneNumber ?? userToChange.PhoneNumber;
        }

        public void Remove(Guid userId)
        {
            var user = TryFindByUserId(userId);
            users.Remove(user);
        }
    }


}
