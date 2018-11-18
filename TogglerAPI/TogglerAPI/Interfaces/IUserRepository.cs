using System.Collections.Generic;
using TogglerAPI.Models;

namespace UserrAPI.Interfaces
{
    public interface IUserRepository
    {
        int CreateUser(int roleId, string username, string password);
        bool UpdateUser(int id, int roleId, string password);
        bool DeleteUser(int id);
        User GetUser(int id);
        List<User> GetUserList();
    }
}
