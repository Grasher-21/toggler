using System;
using System.Collections.Generic;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.Interfaces
{
    public interface IServiceService
    {
        Guid CreateService(ServiceRequestModel serviceRequestModel);
        bool DeleteService(Guid id);
        ServiceResponseModel GetService(Guid id);
        List<ServiceResponseModel> GetServiceList();
        bool UpdateService(ServiceRequestModel serviceRequestModel);
    }
}
