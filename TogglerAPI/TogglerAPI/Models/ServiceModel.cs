using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TogglerAPI.Models
{
    public class ServiceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ServiceId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Version { get; set; }
    }
}
