using System;

namespace TogglerAPI.RequestModels
{
    public class ServiceRequestModel
    {
        public Guid? ServiceId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
