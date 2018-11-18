using System;
using System.Collections.Generic;
using System.Linq;
using TogglerAPI.Enums;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;
using TogglerAPI.Utilities;

namespace TogglerAPI.Repositories
{
    public class ToggleServiceRepository : IToggleServiceRepository
    {
        private readonly TogglerContext TogglerContext;

        public ToggleServiceRepository(TogglerContext togglerContext)
        {
            TogglerContext = togglerContext;
        }

        public bool CreatePermission(int toggleId, Guid serviceId, State state, bool overridenValue)
        {
            try
            {
                ToggleServicePermissionModel toggleServicePermission = new ToggleServicePermissionModel()
                {
                    ToggleId = toggleId,
                    ServiceId = serviceId,
                    State = state,
                    OverridenValue = overridenValue
                };

                TogglerContext.ToggleServicePermissions.Add(toggleServicePermission);
                TogglerContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LoggerFile.LogFile($"Failed to create the ToggleServicePermission: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        public bool DeletePermission(int toggleId, Guid serviceId)
        {
            try
            {
                ToggleServicePermissionModel toggleServicePermission = new ToggleServicePermissionModel()
                {
                    ToggleId = toggleId,
                    ServiceId = serviceId
                };

                TogglerContext.ToggleServicePermissions.Attach(toggleServicePermission);
                TogglerContext.ToggleServicePermissions.Remove(toggleServicePermission);
                TogglerContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LoggerFile.LogFile($"Failed to delete the ToggleServicePermission: {ex.Message}");

                return false;
            }
        }

        public ToggleServicePermissionModel GetToggleServicePermission(int toggleId, Guid serviceId)
        {
            return TogglerContext.ToggleServicePermissions.Find(toggleId, serviceId);
        }

        public List<ToggleServicePermissionModel> GetToggleServiceList()
        {
            return TogglerContext.ToggleServicePermissions.ToList();
        }

        public bool UpdatePermission(int toggleId, Guid serviceId, State state, bool overridenValue)
        {
            ToggleServicePermissionModel toggleServicePermission = TogglerContext.ToggleServicePermissions.Find(toggleId, serviceId);

            try
            {
                if (toggleServicePermission != null)
                {
                    toggleServicePermission.State = state;
                    toggleServicePermission.OverridenValue= overridenValue;

                    TogglerContext.ToggleServicePermissions.Update(toggleServicePermission);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LoggerFile.LogFile($"Failed to update the ToggleServicePermission: {ex.Message}");
            }

            return false;
        }
    }
}
