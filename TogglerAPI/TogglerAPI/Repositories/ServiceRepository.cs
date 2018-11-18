using System;
using System.Collections.Generic;
using System.Linq;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;
using TogglerAPI.Utilities;

namespace TogglerAPI.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly TogglerContext TogglerContext;

        public ServiceRepository(TogglerContext togglerContext)
        {
            TogglerContext = togglerContext;
        }

        public Guid CreateService(string name, string version)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(version))
            {
                throw new ArgumentNullException();
            }

            Service service = new Service() { Name = name, Version = version };

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
                Service service = new Service() { ServiceId = id };

                TogglerContext.Services.Attach(service);
                TogglerContext.Services.Remove(service);
                TogglerContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LoggerFile.LogFile($"Failed to delete the Service: {ex.Message}");

                return false;
            }
        }

        public Service GetService(Guid id)
        {
            return TogglerContext.Services.Find(id);
        }

        public List<Service> GetServiceList()
        {
            return TogglerContext.Services.ToList();
        }

        public bool UpdateService(Guid id, string name, string version)
        {
            Service service = TogglerContext.Services.Find(id);

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
                LoggerFile.LogFile($"Failed to update the Service: {ex.Message}");
            }

            return false;
        }
    }
}
