using OnlineShopWebApp.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public class RolesStorage : IRolesStorage
    {
        private readonly List<Role> roles = new List<Role>();

        public List<Role> GetAll()
        {
            return roles;
        }

        public Role TryFindRoleByName(string name)
        {
            return roles.FirstOrDefault(x => x.Name == name);
        }

        public void Remove(string roleName)
        {
            Role existingRole = TryFindRoleByName(roleName);
            if (existingRole != null)
            {
                roles.Remove(existingRole);
            }
        }

        public void Add(Role role)
        {
            Role foundedRole = TryFindRoleByName(role.Name);
            if (foundedRole == null)
            {
                roles.Add(role);
            }
        }

    }
}
