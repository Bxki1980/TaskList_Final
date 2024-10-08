// Models/UserModel.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskList_Final_.Models
{
    public class UserModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        // Plain text password (used only during account creation and login)
        [Required]
        public string Password { get; set; }

    }
}

