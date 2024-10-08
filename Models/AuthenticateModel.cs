// Models/AuthenticateModel.cs

using System.ComponentModel.DataAnnotations;

namespace TaskList_Final_.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
