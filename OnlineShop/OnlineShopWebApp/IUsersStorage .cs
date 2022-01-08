using OnlineShopWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public interface IUsersStorage
    {
        List<UserViewModel> GetAll(); 
        UserViewModel TryGetByUserLogin(string userLogin);

        UserViewModel TryFindByUserId(Guid userId);

        bool Add(UserViewModel user);
        void ChangePassword(Guid id, string newPassword);
        void EditUserInfo(EditData newData);
        void Remove(Guid userId);
    }
}