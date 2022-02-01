using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
