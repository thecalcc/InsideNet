using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
