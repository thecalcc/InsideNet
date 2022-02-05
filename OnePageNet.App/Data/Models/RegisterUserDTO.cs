using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models;

public class RegisterUserDto
{
    [Required] [EmailAddress] public string Email { get; set; }

    public int MyProperty { get; set; }
}