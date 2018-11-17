using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TogglerAPI.Enums;

namespace TogglerAPI.Models
{
    public class ToggleRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ToggleId { get; set; }
        //[Key]
        public int ServiceId { get; set; }
        public State State { get; set; }
    }
}
