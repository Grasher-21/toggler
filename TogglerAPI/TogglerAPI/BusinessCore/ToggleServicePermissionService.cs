using System;
using System.Collections.Generic;
using TogglerAPI.Enums;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.BusinessCore
{
    public class ToggleServicePermissionService : IToggleServicePermissionService
    {
        private readonly IToggleServicePermissionRepository ToggleServicePermissionRepository;
        private readonly IToggleRepository ToggleRepository;
        private readonly IServiceRepository ServiceRepository;
        private readonly ILogger Logger;

        public ToggleServicePermissionService(IToggleServicePermissionRepository toggleServicePermissionRepository,
            IToggleRepository toggleRepository, IServiceRepository serviceRepository, ILogger logger)
        {
            ToggleServicePermissionRepository = toggleServicePermissionRepository;
            ToggleRepository = toggleRepository;
            ServiceRepository = serviceRepository;
            Logger = logger;
        }

        public bool CreatePermission(ToggleServicePermissionRequestModel toggleServiceRequestModel)
        {
            if (toggleServiceRequestModel == null || toggleServiceRequestModel.ServiceId == Guid.Empty)
            {
                return false;
            }

            try
            {
                if (toggleServiceRequestModel.State == State.ALLOWED ||
                    toggleServiceRequestModel.State == State.BLOCKED ||
                    toggleServiceRequestModel.State == State.OVERRIDE)
                {
                    ToggleServicePermissionModel toggleServicePermissionModel = new ToggleServicePermissionModel()
                    {
                        ToggleId = toggleServiceRequestModel.ToggleId,
                        ServiceId = toggleServiceRequestModel.ServiceId,
                        State = toggleServiceRequestModel.State,
                        OverridenValue = toggleServiceRequestModel.OverridenValue
                    };

                    return ToggleServicePermissionRepository.CreatePermission(toggleServicePermissionModel);
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error creating a ToggleServicePermission: {ex.Message}");
            }

            return false;
        }

        public bool DeletePermission(int toggleId, Guid serviceId)
        {
            if (serviceId == Guid.Empty)
            {
                return false;
            }

            try
            {
                return ToggleServicePermissionRepository.DeletePermission(toggleId, serviceId);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error deleting a ToggleServicePermission: {ex.Message}");

                return false;
            }
        }

        public List<ToggleResponseModel> GetTogglePermissionListForServiceId(Guid serviceId)
        {
            if (serviceId == Guid.Empty)
            {
                return null;
            }

            try
            {
                // List with all Toggles' permissions for a Service (includes Allowed, Blocked and Override Toggles)
                List<ToggleServicePermissionModel> toggleServicePermissionModelList = ToggleServicePermissionRepository.
                    GetTogglePermissionListForServiceId(serviceId);

                if (toggleServicePermissionModelList != null)
                {
                    List<ToggleResponseModel> toggleResponseModelList = new List<ToggleResponseModel>();

                    List<int> idList = new List<int>();

                    foreach (var item in toggleServicePermissionModelList)
                    {
                        if (item.State == State.ALLOWED)
                        {
                            idList.Add(item.ToggleId);
                        }
                        else if (item.State == State.OVERRIDE)
                        {
                        }
                    }

                    // List that contains all "Allowed" Toggles for a specific Service
                    List<ToggleModel> toggleModelList = ToggleRepository.GetToggleListByIds(idList);

                    if (toggleModelList != null)
                    {
                        foreach (var item in toggleModelList)
                        {

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.LogFile($"Error getting TogglePermissionListForServiceId: {ex.Message}");
            }

            return null;
        }

        public ToggleServicePermissionResponseModel GetToggleServicePermission(int toggleId, Guid serviceId)
        {
            if (serviceId == Guid.Empty)
            {
                return null;
            }

            ToggleServicePermissionResponseModel toggleServicePermissionResponseModel;

            try
            {
                ToggleServicePermissionModel toggleServicePermissionModel = ToggleServicePermissionRepository.
                    GetToggleServicePermission(toggleId, serviceId);

                if (toggleServicePermissionModel != null)
                {
                    toggleServicePermissionResponseModel = new ToggleServicePermissionResponseModel()
                    {
                        ToggleId = toggleServicePermissionModel.ToggleId,
                        ServiceId = toggleServicePermissionModel.ServiceId,
                        State = toggleServicePermissionModel.State,
                        OverridenValue = toggleServicePermissionModel.OverridenValue
                    };

                    return toggleServicePermissionResponseModel;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting a ToggleServicePermission: {ex.Message}");
            }

            return null;
        }

        public List<ToggleServicePermissionResponseModel> GetToggleServicePermissionList()
        {
            List<ToggleServicePermissionResponseModel> toggleServicePermissionResponseList;

            try
            {
                List<ToggleServicePermissionModel> toggleServicePermissionModelList = ToggleServicePermissionRepository.GetToggleServicePermissionList();

                if (toggleServicePermissionModelList != null)
                {
                    toggleServicePermissionResponseList = new List<ToggleServicePermissionResponseModel>();

                    foreach (ToggleServicePermissionModel item in toggleServicePermissionModelList)
                    {
                        toggleServicePermissionResponseList.Add(new ToggleServicePermissionResponseModel()
                        {
                            ToggleId = item.ToggleId,
                            ServiceId = item.ServiceId,
                            State = item.State,
                            OverridenValue = item.OverridenValue
                        });
                    }

                    return toggleServicePermissionResponseList;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting a ToggleServicePermission list: {ex.Message}");
            }

            return null;
        }

        public bool UpdatePermission(ToggleServicePermissionRequestModel toggleServiceRequestModel)
        {
            if (toggleServiceRequestModel == null || toggleServiceRequestModel.ServiceId == Guid.Empty)
            {
                return false;
            }

            try
            {
                if (toggleServiceRequestModel.State == State.ALLOWED ||
                    toggleServiceRequestModel.State == State.BLOCKED ||
                    toggleServiceRequestModel.State == State.OVERRIDE)
                {
                    ToggleServicePermissionModel toggleServicePermissionModel = new ToggleServicePermissionModel()
                    {
                        ToggleId = toggleServiceRequestModel.ToggleId,
                        ServiceId = toggleServiceRequestModel.ServiceId,
                        State = toggleServiceRequestModel.State,
                        OverridenValue = toggleServiceRequestModel.OverridenValue
                    };

                    return ToggleServicePermissionRepository.UpdatePermission(toggleServicePermissionModel);
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error updating a ToggleServicePermission: {ex.Message}");
            }

            return false;
        }
    }
}
