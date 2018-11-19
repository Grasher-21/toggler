using System;
using System.Collections.Generic;
using TogglerAPI.Models;

namespace TogglerAPI.Interfaces
{
    public interface IToggleServicePermissionRepository
    {
        bool CreatePermission(ToggleServicePermissionModel toggleServiceModel);
        bool DeletePermission(int toggleId, Guid serviceId);
        ToggleServicePermissionModel GetToggleServicePermission(int toggleId, Guid serviceId);
        List<ToggleServicePermissionModel> GetToggleServicePermissionList();
        List<ToggleServicePermissionModel> GetTogglePermissionListForServiceId(Guid serviceId);
        bool UpdatePermission(ToggleServicePermissionModel toggleServiceModel);
    }
}
