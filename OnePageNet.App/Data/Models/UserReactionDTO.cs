using OnePageNet.App.Data.Enums;

namespace OnePageNet.App.Data.Models
{
    public class UserReactionDTO
    {
        public ReactionEntity Reaction { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
