namespace InsideNet.Data.Data.Models;

public class UserDto : BaseDto
{
    public string? MediaUri { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DoB { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
}