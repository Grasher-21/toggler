using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TogglerAPI.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [ForeignKey("RoleIdForeignKey")]
        public int RoleId { get; set; }

        [Required, MaxLength(32)]
        public string Username { get; set; }

        [Required, MaxLength(32)]
        public string Password { get; set; }
    }
}
