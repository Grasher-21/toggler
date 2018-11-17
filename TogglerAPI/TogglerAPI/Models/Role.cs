using System.ComponentModel.DataAnnotations.Schema;

namespace TogglerAPI.Models
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
