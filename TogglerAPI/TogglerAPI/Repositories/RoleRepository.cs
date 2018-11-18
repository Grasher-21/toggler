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

            RoleModel role = new RoleModel() { Name = name, Description = description };

            TogglerContext.Roles.Add(role);
            TogglerContext.SaveChanges();

            return role.RoleId;
        }

        public bool DeleteRole(int id)
        {
            try
            {
                RoleModel role = new RoleModel() { RoleId = id };

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

        public RoleModel GetRole(int id)
        {
            return TogglerContext.Roles.Find(id);
        }

        public List<RoleModel> GetRoleList()
        {
            return TogglerContext.Roles.ToList();
        }

        public bool UpdateRole(int id, string name, string description)
        {
            RoleModel role = TogglerContext.Roles.Find(id);

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
