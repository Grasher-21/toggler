using System;
using System.Collections.Generic;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.BusinessCore
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository RoleRepository;
        private readonly ILogger Logger;

        public RoleService(IRoleRepository roleRepository, ILogger logger)
        {
            RoleRepository = roleRepository;
            Logger = logger;
        }

        public int CreateRole(RoleRequestModel roleRequestModel)
        {
            if (string.IsNullOrWhiteSpace(roleRequestModel.Name) || string.IsNullOrWhiteSpace(roleRequestModel.Description))
            {
                return -1;
            }

            try
            {
                RoleModel roleModel = new RoleModel()
                {
                    Name = roleRequestModel.Name,
                    Description = roleRequestModel.Description
                };

                return RoleRepository.CreateRole(roleModel);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error creating a role: {ex.Message}");

                return -1;
            }
        }

        public bool DeleteRole(int id)
        {
            try
            {
                return RoleRepository.DeleteRole(id);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error deleting a role: {ex.Message}");

                return false;
            }
        }

        public RoleResponseModel GetRole(int id)
        {
            RoleResponseModel roleResponseModel;

            try
            {
                RoleModel roleModel = RoleRepository.GetRole(id);

                if (roleModel != null)
                {
                    roleResponseModel = new RoleResponseModel()
                    {
                        RoleId = roleModel.RoleId,
                        Name = roleModel.Name,
                        Description = roleModel.Description
                    };

                    return roleResponseModel;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting a role: {ex.Message}");
            }

            return null;
        }

        public List<RoleResponseModel> GetRoleList()
        {
            List<RoleResponseModel> roleList;

            try
            {
                List<RoleModel> roleModelList = RoleRepository.GetRoleList();

                if (roleModelList != null)
                {
                    roleList = new List<RoleResponseModel>();

                    foreach (RoleModel item in roleModelList)
                    {
                        roleList.Add(new RoleResponseModel()
                        {
                            RoleId = item.RoleId,
                            Name = item.Name,
                            Description = item.Description
                        });
                    }

                    return roleList;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting a role list: {ex.Message}");
            }

            return null;
        }

        public bool UpdateRole(RoleRequestModel roleRequestModel)
        {
            if (roleRequestModel?.RoleId == null || string.IsNullOrWhiteSpace(roleRequestModel.Name) || string.IsNullOrWhiteSpace(roleRequestModel.Description))
            {
                return false;
            }

            try
            {
                RoleModel roleModel = new RoleModel()
                {
                    RoleId = (int) roleRequestModel.RoleId,
                    Name = roleRequestModel.Name,
                    Description = roleRequestModel.Description
                };

                return RoleRepository.UpdateRole(roleModel);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error updating a role: {ex.Message}");
            }

            return false;
        }
    }
}
