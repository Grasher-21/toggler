using System.Collections.Generic;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.Interfaces
{
    public interface IRoleService
    {
        int CreateRole(RoleRequestModel roleRequestModel);
        bool DeleteRole(int id);
        RoleResponseModel GetRole(int id);
        List<RoleResponseModel> GetRoleList();
        bool UpdateRole(RoleRequestModel roleRequestModel);
    }
}
