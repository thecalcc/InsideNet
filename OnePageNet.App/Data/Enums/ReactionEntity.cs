using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Data.Enums
{
    public class ReactionEntity : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserReactionEntity> Users { get; set; }
    }
}