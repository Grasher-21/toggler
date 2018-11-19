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

        public List<ToggleResponseModel> GetTogglesListForServiceId(Guid serviceId)
        {
            if (serviceId == Guid.Empty)
            {
                return null;
            }

            try
            {
                return GetTogglesDetailsForServiceId(serviceId);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting TogglePermissionListForServiceId: {ex.Message}");

                return null;
            }
        }

        private List<ToggleResponseModel> GetTogglesDetailsForServiceId(Guid serviceId)
        {
            // List with all Toggles' permissions for a Service (includes Allowed, Blocked and Override Toggles)
            List<ToggleServicePermissionModel> toggleServicePermissionModelList = ToggleServicePermissionRepository.
                    GetTogglePermissionListForServiceId(serviceId);

            if (toggleServicePermissionModelList?.Count > 0)
            {
                List<ToggleResponseModel> toggleResponseModelList = new List<ToggleResponseModel>();

                List<int> allowedIdList = new List<int>();
                List<int> overriddenIdList = new List<int>();

                foreach (var item in toggleServicePermissionModelList)
                {
                    if (item.State == State.ALLOWED)
                    {
                        allowedIdList.Add(item.ToggleId);
                    }
                    else if (item.State == State.OVERRIDE)
                    {
                        overriddenIdList.Add(item.ToggleId);

                        toggleResponseModelList.Add(new ToggleResponseModel()
                        {
                            ToggleId = item.ToggleId,
                            Value = item.OverridenValue
                        });
                    }
                }

                List<ToggleModel> overridenList = ToggleRepository.GetToggleListByIds(overriddenIdList);
                List<ToggleModel> allowedList = ToggleRepository.GetToggleListByIds(allowedIdList);

                if (overridenList != null)
                {
                    for (int i = 0; i < toggleResponseModelList.Count; i++)
                    {
                        for (int j = 0; j < overridenList.Count; j++)
                        {
                            if (toggleResponseModelList[i].ToggleId == overridenList[j].ToggleId)
                            {
                                toggleResponseModelList[i].Name = overridenList[j].Name;
                                break;
                            }
                        }
                    }
                }

                foreach (var item in allowedList)
                {
                    toggleResponseModelList.Add(new ToggleResponseModel()
                    {
                        ToggleId = item.ToggleId,
                        Name = item.Name,
                        Value = item.Value
                    });
                }

                return toggleResponseModelList;
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
