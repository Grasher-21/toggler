using System;

namespace TogglerAPI.ResponseModels
{
    public class ServiceResponseModel
    {
        public Guid ServiceId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
