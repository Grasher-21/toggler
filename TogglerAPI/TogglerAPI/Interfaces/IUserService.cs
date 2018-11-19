using System.Collections.Generic;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.Interfaces
{
    public interface IUserService
    {
        int CreateUser(UserRequestModel userModel);
        bool DeleteUserById(int id);
        bool DeleteUserByUsername(string username);
        UserResponseModel GetUserById(int id);
        UserResponseModel GetUserByUsername(string username);
        List<UserResponseModel> GetUserList();
        bool UpdateUser(UserRequestModel userModel);
    }
}
