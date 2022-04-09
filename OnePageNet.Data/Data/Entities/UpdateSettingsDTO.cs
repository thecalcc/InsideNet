using OnePageNet.Data.Data.Models;

namespace OnePageNet.Data.Data.Entities;

public class UpdateSettingsDTO : BaseDTO
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}