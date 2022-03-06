using System.ComponentModel.DataAnnotations;

namespace OnePageNet.Data.Data.Models;

public class ForgotPasswordDto
{
    [Required] [EmailAddress] public string Email { get; set; }
}