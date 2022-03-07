namespace OnePageNet.Data.Data.Entities;

public class UserReactionEntity : BaseEntity
{
    public virtual ReactionEntity Reaction { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
}