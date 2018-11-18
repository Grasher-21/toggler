using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TogglerAPI.Enums;

namespace TogglerAPI.Models
{
    public class ToggleServicePermissionModel
    {
        [ForeignKey("Toggles"), Column(Order = 0)]
        public int ToggleId { get; set; }

        [ForeignKey("Services"), Column(Order = 1)]
        public Guid ServiceId { get; set; }

        [Required]
        public State State { get; set; }

        public bool OverridenValue { get; set; }
    }
}
