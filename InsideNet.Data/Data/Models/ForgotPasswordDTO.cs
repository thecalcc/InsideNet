using System.ComponentModel.DataAnnotations;

namespace InsideNet.Data.Data.Models;

public class ForgotPasswordDto
{
    [Required] [EmailAddress] public string Email { get; set; }
}