using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Data.Models;

public class UserGroupDto : BaseDTO
{
    public ICollection<GroupDTO> Group { get; set; }
    public ICollection<ApplicationUser> Users { get; set; }
}