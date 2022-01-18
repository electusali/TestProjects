using System.ComponentModel.DataAnnotations;

namespace TestProjects.WebUI.Models.Security
{
    public class LoginViewModal
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
