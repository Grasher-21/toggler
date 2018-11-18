using System.Collections.Generic;
using TogglerAPI.Models;

namespace TogglerAPI.Interfaces
{
    public interface IUserRepository
    {
        int CreateUser(int roleId, string username, string password);
        bool UpdateUser(int id, int roleId, string password);
        bool DeleteUser(int id);
        UserModel GetUser(int id);
        List<UserModel> GetUserList();
    }
}
