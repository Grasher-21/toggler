using System.Collections.Generic;
using TogglerAPI.Models;

namespace TogglerAPI.Interfaces
{
    public interface IRoleRepository
    {
        int CreateRole(string name, string description);
        bool UpdateRole(int id, string name, string description);
        bool DeleteRole(int id);
        RoleModel GetRole(int id);
        List<RoleModel> GetRoleList();
    }
}