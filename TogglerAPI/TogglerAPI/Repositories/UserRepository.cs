using System;
using System.Collections.Generic;
using System.Linq;
using TogglerAPI.Models;
using TogglerAPI.Utilities;
using TogglerAPI.Interfaces;

namespace TogglerAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TogglerContext TogglerContext;

        public UserRepository(TogglerContext togglerContext)
        {
            TogglerContext = togglerContext;
        }

        public int CreateUser(int roleId, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException();
            }

            RoleModel role = TogglerContext.Roles.Find(roleId);

            if (role == null)
            {
                throw new ArgumentException($"Couldn't find a Role with the ID {roleId}");
            }

            UserModel User = new UserModel() { RoleId = roleId, Username = username, Password = password };

            TogglerContext.Users.Add(User);
            TogglerContext.SaveChanges();

            return User.UserId;
        }

        public bool DeleteUser(int id)
        {
            try
            {
                UserModel User = new UserModel() { UserId = id };

                TogglerContext.Users.Attach(User);
                TogglerContext.Users.Remove(User);
                TogglerContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LoggerFile.LogFile($"Failed to delete the User: {ex.Message}");

                return false;
            }
        }

        public UserModel GetUser(int id)
        {
            return TogglerContext.Users.Find(id);
        }

        public List<UserModel> GetUserList()
        {
            return TogglerContext.Users.ToList();
        }

        public bool UpdateUser(int id, int roleId, string password)
        {
            UserModel user = TogglerContext.Users.Find(id);
            RoleModel role = TogglerContext.Roles.Find(roleId);

            try
            {
                if (user != null && role != null)
                {
                    user.RoleId = roleId;
                    user.Password = password;

                    TogglerContext.Users.Update(user);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LoggerFile.LogFile($"Failed to update the User: {ex.Message}");
            }

            return false;
        }
    }
}
