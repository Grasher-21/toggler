using System;
using System.Collections.Generic;
using TogglerAPI.Enums;
using TogglerAPI.Models;

namespace TogglerAPI.Interfaces
{
    public interface IToggleServiceRepository
    {
        bool CreatePermission(int toggleId, Guid serviceId, State state, bool overridenValue);
        bool UpdatePermission(int toggleId, Guid serviceId, State state, bool overridenValue);
        bool DeletePermission(int toggleId, Guid serviceId);
        ToggleServicePermissionModel GetToggleServicePermission(int toggleId, Guid serviceId);
        List<ToggleServicePermissionModel> GetToggleServiceList();
    }
}
