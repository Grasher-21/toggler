using System;
using System.Collections.Generic;
using System.Linq;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;

namespace TogglerAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TogglerContext TogglerContext;
        private readonly ILogger Logger;
        private readonly IRoleRepository RoleRepository;

        public UserRepository(TogglerContext togglerContext, ILogger logger, IRoleRepository roleRepository)
        {
            TogglerContext = togglerContext;
            Logger = logger;
            RoleRepository = roleRepository;
        }

        public int CreateUser(UserModel userModel)
        {
            if (userModel == null || string.IsNullOrWhiteSpace(userModel.Username) || string.IsNullOrWhiteSpace(userModel.Password))
            {
                throw new ArgumentNullException();
            }

            try
            {
                RoleModel role = RoleRepository.GetRole(userModel.RoleId);

                if (role == null)
                {
                    Logger.LogFile($"Failed to find the Role {userModel.RoleId}");

                    return -1;
                }

                UserModel user = new UserModel()
                {
                    RoleId = userModel.RoleId,
                    Username = userModel.Username,
                    Password = userModel.Password
                };

                TogglerContext.Users.Add(user);
                TogglerContext.SaveChanges();

                return user.UserId;
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to delete the User: {ex.Message}");

                return -1;
            }
        }

        public bool DeleteUserById(int id)
        {
            try
            {
                UserModel user = new UserModel() { UserId = id };

                TogglerContext.Users.Attach(user);
                TogglerContext.Users.Remove(user);
                TogglerContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to delete the User: {ex.Message}");

                return false;
            }
        }

        public bool DeleteUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException();
            }

            try
            {
                UserModel user = TogglerContext.Users.Where(u => u.Username == username).FirstOrDefault();

                TogglerContext.Users.Attach(user);
                TogglerContext.Users.Remove(user);
                TogglerContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to delete the User: {ex.Message}");

                return false;
            }
        }

        public UserModel GetUserById(int id)
        {
            try
            {
                return TogglerContext.Users.Find(id);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to get the User by id = {id}: {ex.Message}");

                return null;
            }
        }

        public UserModel GetUserByUsername(string username)
        {
            try
            {
                return TogglerContext.Users.Where(u => u.Username == username).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to get the User by username = {username}: {ex.Message}");

                return null;
            }
        }

        public List<UserModel> GetUserList()
        {
            try
            {
                return TogglerContext.Users.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to get the User list: {ex.Message}");

                return null;
            }
        }

        public bool UpdateUser(UserModel userModel)
        {
            if (userModel == null || string.IsNullOrWhiteSpace(userModel.Password))
            {
                throw new ArgumentNullException();
            }

            try
            {
                UserModel user = TogglerContext.Users.Find(userModel.UserId);
                RoleModel role = RoleRepository.GetRole(userModel.RoleId);

                if (user != null && role != null)
                {
                    user.RoleId = userModel.RoleId;
                    user.Password = userModel.Password;

                    TogglerContext.Users.Update(user);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to update the User: {ex.Message}");
            }

            return false;
        }
    }
}
