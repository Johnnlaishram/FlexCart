using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexCart.Models
{
    [Table("admin")]
    public class Admin
    {
        // Explicitly marking adminId as the primary key
        [Key]
        public int AdminId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
