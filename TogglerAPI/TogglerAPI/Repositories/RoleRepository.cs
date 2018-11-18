using System;
using System.Collections.Generic;
using System.Linq;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;
using TogglerAPI.Utilities;

namespace TogglerAPI.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TogglerContext TogglerContext;

        public RoleRepository(TogglerContext togglerContext)
        {
            TogglerContext = togglerContext;
        }

        public int CreateRole(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException();
            }

            Role role = new Role() { Name = name, Description = description };

            TogglerContext.Roles.Add(role);
            TogglerContext.SaveChanges();

            return role.RoleId;
        }

        public bool DeleteRole(int id)
        {
            try
            {
                Role role = new Role() { RoleId = id };

                TogglerContext.Roles.Attach(role);
                TogglerContext.Roles.Remove(role);
                TogglerContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LoggerFile.LogFile($"Failed to delete the Role: {ex.Message}");

                return false;
            }
        }

        public Role GetRole(int id)
        {
            return TogglerContext.Roles.Find(id);
        }

        public List<Role> GetRoleList()
        {
            return TogglerContext.Roles.ToList();
        }

        public bool UpdateRole(int id, string name, string description)
        {
            Role role = TogglerContext.Roles.Find(id);

            try
            {
                if (role != null)
                {
                    role.Name = name;
                    role.Description = description;

                    TogglerContext.Roles.Update(role);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LoggerFile.LogFile($"Failed to update the Role: {ex.Message}");
            }

            return false;
        }
    }
}
