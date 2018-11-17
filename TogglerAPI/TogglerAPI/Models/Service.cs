using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TogglerAPI.Models
{
    public class Service
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }
        public Guid Token { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
