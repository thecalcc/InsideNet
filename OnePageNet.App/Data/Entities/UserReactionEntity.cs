using OnePageNet.App.Data.Enums;

namespace OnePageNet.App.Data.Entities;

public class UserReactionEntity : BaseEntity
{
    public virtual ReactionEntity Reaction { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
}