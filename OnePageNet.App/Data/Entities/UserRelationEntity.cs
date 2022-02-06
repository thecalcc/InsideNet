namespace OnePageNet.App.Data.Entities;

public class UserRelationEntity : BaseEntity
{
    public RelationEntity UserRelationship { get; set; }
    public ApplicationUser CurrentUser { get; set; }
    public ApplicationUser TargetUser { get; set; }
}