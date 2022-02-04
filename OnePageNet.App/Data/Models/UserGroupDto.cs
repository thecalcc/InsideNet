namespace OnePageNet.App.Data.Models;

public class UserGroupDto : BaseDto
{
    public ICollection<GroupDto> Group { get; set; }
    public ICollection<ApplicationUser> Users { get; set; }
}