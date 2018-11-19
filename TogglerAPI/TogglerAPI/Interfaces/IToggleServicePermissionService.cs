using System;
using System.Collections.Generic;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.Interfaces
{
    public interface IToggleServicePermissionService
    {
        bool CreatePermission(ToggleServicePermissionRequestModel toggleServiceRequestModel);
        bool DeletePermission(int toggleId, Guid serviceId);
        ToggleServicePermissionResponseModel GetToggleServicePermission(int toggleId, Guid serviceId);
        List<ToggleServicePermissionResponseModel> GetToggleServicePermissionList();
        List<ToggleResponseModel> GetTogglePermissionListForServiceId(Guid serviceId);
        bool UpdatePermission(ToggleServicePermissionRequestModel toggleServiceRequestModel);
    }
}
