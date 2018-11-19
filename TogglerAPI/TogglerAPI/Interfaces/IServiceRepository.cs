using System;
using System.Collections.Generic;
using TogglerAPI.Models;

namespace TogglerAPI.Interfaces
{
    public interface IServiceRepository
    {
        Guid CreateService(ServiceModel serviceRequestModel);
        bool DeleteService(Guid id);
        ServiceModel GetService(Guid id);
        List<ServiceModel> GetServiceList();
        bool UpdateService(ServiceModel serviceRequestModel);
    }
}
