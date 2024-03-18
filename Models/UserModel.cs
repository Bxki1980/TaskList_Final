using System.ComponentModel.DataAnnotations;

namespace TaskList_Final_.Models
{
    public class UserModel
    {
        [Key]
        public int ID { get; set; }

        // Other properties
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}