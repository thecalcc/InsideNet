using OnePageNet.App.Data.Enums;

namespace OnePageNet.App.Data.Models;

public class UserRelationDto : BaseDto
{
    public UserRelation UserRelationship { get; set; }
    // Used string IDs - relate to ApplicationUser
    public ICollection<string> CurrentUserId { get; set; }
    public ICollection<string> TargetUserId { get; set; }
}