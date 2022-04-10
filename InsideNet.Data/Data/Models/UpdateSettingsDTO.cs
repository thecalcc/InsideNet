namespace InsideNet.Data.Data.Models;

public class UpdateSettingsDto : BaseDto
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}