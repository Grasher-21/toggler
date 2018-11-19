using System;
using System.Collections.Generic;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.BusinessCore
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly ILogger Logger;

        public UserService(IUserRepository userRepository, ILogger logger)
        {
            UserRepository = userRepository;
            Logger = logger;
        }

        public int CreateUser(UserRequestModel userRequestModel)
        {
            if (userRequestModel == null || string.IsNullOrWhiteSpace(userRequestModel.Username) || string.IsNullOrWhiteSpace(userRequestModel.Password))
            {
                return -1;
            }

            try
            {
                UserModel userModel = new UserModel()
                {
                    RoleId = userRequestModel.RoleId,
                    Username = userRequestModel.Username,
                    Password = userRequestModel.Password
                };

                return UserRepository.CreateUser(userModel);
            }
            catch(Exception ex)
            {
                Logger.LogFile($"Error creating a User: {ex.Message}");

                return -1;
            }
        }

        public bool DeleteUserById(int id)
        {
            try
            {
                return UserRepository.DeleteUserById(id);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error deleting a User by id = {id}: {ex.Message}");

                return false;
            }
        }

        public bool DeleteUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            try
            {
                return UserRepository.DeleteUserByUsername(username);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error deleting a User by username = {username}: {ex.Message}");

                return false;
            }
        }

        public UserResponseModel GetUserById(int id)
        {
            UserResponseModel userResponseModel;

            try
            {
                UserModel userModel = UserRepository.GetUserById(id);

                if (userModel != null)
                {
                    userResponseModel = new UserResponseModel()
                    {
                        UserId = userModel.UserId,
                        RoleId = userModel.RoleId,
                        Username = userModel.Username,
                        Password = userModel.Password
                    };

                    return userResponseModel;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting a User by id = {id}: {ex.Message}");
            }

            return null;
        }

        public UserResponseModel GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            UserResponseModel userResponseModel;

            try
            {
                UserModel userModel = UserRepository.GetUserByUsername(username);

                if (userModel != null)
                {
                    userResponseModel = new UserResponseModel()
                    {
                        UserId = userModel.UserId,
                        RoleId = userModel.RoleId,
                        Username = userModel.Username,
                        Password = userModel.Password
                    };

                    return userResponseModel;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting a User by username = {username}: {ex.Message}");
            }

            return null;
        }

        public List<UserResponseModel> GetUserList()
        {
            List<UserResponseModel> userList;

            try
            {
                List<UserModel> userModelList = UserRepository.GetUserList();

                if (userModelList != null)
                {
                    userList = new List<UserResponseModel>();

                    foreach (UserModel item in userModelList)
                    {
                        userList.Add(new UserResponseModel()
                        {
                            UserId = item.UserId,
                            RoleId = item.RoleId,
                            Username = item.Username,
                            Password = item.Password
                        });
                    }

                    return userList;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting a User list: {ex.Message}");
            }

            return null;
        }

        public bool UpdateUser(UserRequestModel userRequestModel)
        {
            if (userRequestModel?.UserId == null || string.IsNullOrWhiteSpace(userRequestModel.Password))
            {
                return false;
            }

            try
            {
                UserModel userModel = new UserModel()
                {
                    UserId = (int)userRequestModel.UserId,
                    RoleId = userRequestModel.RoleId,
                    Password = userRequestModel.Password
                };

                return UserRepository.UpdateUser(userModel);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error updating a User: {ex.Message}");
            }

            return false;
        }
    }
}
