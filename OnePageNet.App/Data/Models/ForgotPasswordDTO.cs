using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models;

public class ForgotPasswordDto
{
    [Required] [EmailAddress] public string Email { get; set; }
}