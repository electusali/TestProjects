using System.ComponentModel.DataAnnotations;

namespace TestProjects.WebUI.Models.Security
{
    public class ForgotPasswordViewModal
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
