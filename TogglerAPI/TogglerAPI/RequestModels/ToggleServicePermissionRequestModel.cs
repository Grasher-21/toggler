using System;
using TogglerAPI.Enums;

namespace TogglerAPI.RequestModels
{
    public class ToggleServicePermissionRequestModel
    {
        public int ToggleId { get; set; }
        public Guid ServiceId { get; set; }
        public State State { get; set; }
        public bool OverridenValue { get; set; }
    }
}
