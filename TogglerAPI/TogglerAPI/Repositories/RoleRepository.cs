using System;
using System.Collections.Generic;
using System.Linq;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;

namespace TogglerAPI.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TogglerContext TogglerContext;
        private readonly ILogger Logger;

        public RoleRepository(TogglerContext togglerContext, ILogger logger)
        {
            TogglerContext = togglerContext;
            Logger = logger;
        }

        public int CreateRole(RoleModel roleModel)
        {
            if (roleModel == null || string.IsNullOrWhiteSpace(roleModel.Name) || string.IsNullOrWhiteSpace(roleModel.Description))
            {
                throw new ArgumentNullException();
            }

            try
            {
                RoleModel role = new RoleModel() { Name = roleModel.Name, Description = roleModel.Description };

                TogglerContext.Roles.Add(role);
                TogglerContext.SaveChanges();

                return role.RoleId;
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to create the Role: {ex.Message}");

                return -1;
            }
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
                Logger.LogFile($"Failed to delete the Role: {ex.Message}");

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

        public bool UpdateRole(RoleModel roleModel)
        {
            if (roleModel == null || string.IsNullOrWhiteSpace(roleModel.Name) || string.IsNullOrWhiteSpace(roleModel.Description))
            {
                throw new ArgumentNullException();
            }

            try
            {
                RoleModel role = TogglerContext.Roles.Find(roleModel.RoleId);

                if (role != null)
                {
                    role.Name = roleModel.Name;
                    role.Description = roleModel.Description;

                    TogglerContext.Roles.Update(role);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to update the Role: {ex.Message}");
            }

            return false;
        }
    }
}
