using System;
using System.Collections.Generic;
using System.Linq;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;

namespace TogglerAPI.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly TogglerContext TogglerContext;
        private readonly ILogger Logger;

        public ServiceRepository(TogglerContext togglerContext, ILogger logger)
        {
            TogglerContext = togglerContext;
            Logger = logger;
        }

        public Guid CreateService(ServiceModel serviceModel)
        {
            if (serviceModel == null || string.IsNullOrWhiteSpace(serviceModel.Name) || string.IsNullOrWhiteSpace(serviceModel.Version))
            {
                throw new ArgumentNullException();
            }

            try
            {
                TogglerContext.Services.Add(serviceModel);
                TogglerContext.SaveChanges();

                return serviceModel.ServiceId;
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to create the Service: {ex.Message}");

                return Guid.Empty;
            }
        }

        public bool DeleteService(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            try
            {
                ServiceModel service = TogglerContext.Services.Find(id);

                if (service != null)
                {
                    TogglerContext.Services.Attach(service);
                    TogglerContext.Services.Remove(service);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to delete the Service: {ex.Message}");
            }

            return false;
        }

        public ServiceModel GetService(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            try
            {
                return TogglerContext.Services.Find(id);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to get the Service with id = {id}: {ex.Message}");

                return null;
            }
        }

        public List<ServiceModel> GetServiceList()
        {
            try
            {
                return TogglerContext.Services.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to get the Service list: {ex.Message}");

                return null;
            }
        }

        public bool UpdateService(ServiceModel serviceModel)
        {
            if (serviceModel == null || serviceModel.ServiceId == Guid.Empty ||
                string.IsNullOrWhiteSpace(serviceModel.Name) || string.IsNullOrWhiteSpace(serviceModel.Version))
            {
                throw new ArgumentNullException();
            }

            try
            {
                ServiceModel service = TogglerContext.Services.Find(serviceModel.ServiceId);

                if (service != null)
                {
                    service.Name = serviceModel.Name;
                    service.Version = serviceModel.Version;

                    TogglerContext.Services.Update(service);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to update the Service: {ex.Message}");
            }

            return false;
        }
    }
}
