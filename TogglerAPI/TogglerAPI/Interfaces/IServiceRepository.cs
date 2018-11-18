using System;
using System.Collections.Generic;
using TogglerAPI.Models;

namespace TogglerAPI.Interfaces
{
    public interface IServiceRepository
    {
        Guid CreateService(string name, string version);
        bool UpdateService(Guid id, string name, string description);
        bool DeleteService(Guid id);
        Service GetService(Guid id);
        List<Service> GetServiceList();
    }
}
