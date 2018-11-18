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

        public Guid CreateService(string name, string version)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(version))
            {
                throw new ArgumentNullException();
            }

            ServiceModel service = new ServiceModel() { Name = name, Version = version };

            TogglerContext.Services.Add(service);
            TogglerContext.SaveChanges();

            return service.ServiceId;
        }

        public bool DeleteService(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            try
            {
                ServiceModel service = new ServiceModel() { ServiceId = id };

                TogglerContext.Services.Attach(service);
                TogglerContext.Services.Remove(service);
                TogglerContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to delete the Service: {ex.Message}");

                return false;
            }
        }

        public ServiceModel GetService(Guid id)
        {
            return TogglerContext.Services.Find(id);
        }

        public List<ServiceModel> GetServiceList()
        {
            return TogglerContext.Services.ToList();
        }

        public bool UpdateService(Guid id, string name, string version)
        {
            ServiceModel service = TogglerContext.Services.Find(id);

            try
            {
                if (service != null)
                {
                    service.Name = name;
                    service.Version = version;

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
