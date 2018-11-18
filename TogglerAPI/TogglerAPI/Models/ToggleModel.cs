using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TogglerAPI.Models
{
    public class ToggleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ToggleId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Value { get; set; }
    }
}
