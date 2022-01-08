using OnlineShopWebApp.Areas.Admin.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public interface IRolesStorage
    {
        List<Role> GetAll();

        Role TryFindRoleByName(string name);

        /// <summary> 
        /// Checks if such role name exists in rolesStorage, then deletes.
        /// if there is no such roleName - do nothing 
        /// </summary>
        void Remove(string roleName);

        void Add(Role role);
    }
}