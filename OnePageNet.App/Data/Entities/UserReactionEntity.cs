using OnePageNet.App.Data.Enums;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Models;

namespace OnePageNet.App.Data.Entities
{
    public class UserReactionEntity : BaseEntity
    {
        public virtual ReactionEntity Reaction { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}