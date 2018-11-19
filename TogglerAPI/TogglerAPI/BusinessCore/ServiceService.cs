using System;
using System.Collections.Generic;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.BusinessCore
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository ServiceRepository;
        private readonly ILogger Logger;

        public ServiceService(IServiceRepository serviceRepository, ILogger logger)
        {
            ServiceRepository = serviceRepository;
            Logger = logger;
        }

        public Guid CreateService(ServiceRequestModel serviceRequestModel)
        {
            if (serviceRequestModel == null || string.IsNullOrWhiteSpace(serviceRequestModel.Name) || string.IsNullOrWhiteSpace(serviceRequestModel.Version))
            {
                return Guid.Empty;
            }

            try
            {
                ServiceModel serviceModel = new ServiceModel()
                {
                    Name = serviceRequestModel.Name,
                    Version = serviceRequestModel.Version
                };

                return ServiceRepository.CreateService(serviceModel);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error creating a service: {ex.Message}");

                return Guid.Empty;
            }
        }

        public bool DeleteService(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            try
            {
                return ServiceRepository.DeleteService(id);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error deleting a service: {ex.Message}");

                return false;
            }
        }

        public ServiceResponseModel GetService(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            ServiceResponseModel serviceResponseModel;

            try
            {
                ServiceModel serviceModel = ServiceRepository.GetService(id);

                if (serviceModel != null)
                {
                    serviceResponseModel = new ServiceResponseModel()
                    {
                        ServiceId = serviceModel.ServiceId,
                        Name = serviceModel.Name,
                        Version = serviceModel.Version
                    };

                    return serviceResponseModel;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting a service by id = {id}: {ex.Message}");
            }

            return null;
        }

        public List<ServiceResponseModel> GetServiceList()
        {
            List<ServiceResponseModel> serviceList;

            try
            {
                List<ServiceModel> serviceModelList = ServiceRepository.GetServiceList();

                if (serviceModelList != null)
                {
                    serviceList = new List<ServiceResponseModel>();

                    foreach (ServiceModel item in serviceModelList)
                    {
                        serviceList.Add(new ServiceResponseModel()
                        {
                            ServiceId = item.ServiceId,
                            Name = item.Name,
                            Version = item.Version
                        });
                    }

                    return serviceList;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting a service list: {ex.Message}");
            }

            return null;
        }

        public bool UpdateService(ServiceRequestModel serviceRequestModel)
        {
            if (serviceRequestModel == null || serviceRequestModel.ServiceId == Guid.Empty || 
                string.IsNullOrWhiteSpace(serviceRequestModel.Name) || string.IsNullOrWhiteSpace(serviceRequestModel.Version))
            {
                return false;
            }

            try
            {
                ServiceModel serviceModel = new ServiceModel()
                {
                    ServiceId = (Guid)serviceRequestModel.ServiceId,
                    Name = serviceRequestModel.Name,
                    Version = serviceRequestModel.Version
                };

                return ServiceRepository.UpdateService(serviceModel);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error updating a service: {ex.Message}");
            }

            return false;
        }
    }
}
