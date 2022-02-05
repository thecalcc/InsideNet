using OnePageNet.App.Data.Enums;

namespace OnePageNet.App.Data.Entities;

public class UserRelationEntity : BaseEntity
{
    public UserRelation UserRelationship { get; set; }
    public ApplicationUser CurrentUser { get; set; }
    public ApplicationUser TargetUser { get; set; }
}