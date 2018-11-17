using System.ComponentModel.DataAnnotations.Schema;

namespace TogglerAPI.Models
{
    public class Toggle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ToggleId { get; set; }
        public string Name { get; set; }
        public bool Value { get; set; }
    }
}
