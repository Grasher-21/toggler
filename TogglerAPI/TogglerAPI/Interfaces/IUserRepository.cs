using System.Collections.Generic;
using TogglerAPI.Models;

namespace TogglerAPI.Interfaces
{
    public interface IUserRepository
    {
        int CreateUser(UserModel userModel);
        bool DeleteUserById(int id);
        bool DeleteUserByUsername(string username);
        UserModel GetUserById(int id);
        UserModel GetUserByUsername(string username);
        List<UserModel> GetUserList();
        bool UpdateUser(UserModel userModel);
    }
}
