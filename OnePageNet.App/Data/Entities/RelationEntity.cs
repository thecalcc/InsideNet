namespace OnePageNet.App.Data.Entities;

public class RelationEntity : BaseEntity
{
    public string Name { get; set; }
    public ICollection<UserRelationEntity> Users { get; set; }
}