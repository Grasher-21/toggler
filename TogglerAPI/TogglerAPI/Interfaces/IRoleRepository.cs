using System.Collections.Generic;
using TogglerAPI.Models;

namespace TogglerAPI.Interfaces
{
    public interface IRoleRepository
    {
        int CreateRole(RoleModel roleModel);
        bool DeleteRole(int id);
        RoleModel GetRole(int id);
        List<RoleModel> GetRoleList();
        bool UpdateRole(RoleModel roleModel);
    }
}