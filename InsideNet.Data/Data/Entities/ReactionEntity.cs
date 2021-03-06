namespace InsideNet.Data.Data.Entities;

public class ReactionEntity : BaseEntity
{
    public string Name { get; set; }
    public ICollection<UserReactionEntity> Users { get; set; }
}