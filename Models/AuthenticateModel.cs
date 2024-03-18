using System.ComponentModel.DataAnnotations;

namespace TaskList_Final_.Models
{
    public class AuthenticateModel
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }


        [Required]
        public string Password { get; set; }



    }
}
