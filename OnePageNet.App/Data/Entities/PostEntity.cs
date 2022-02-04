using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Data.Entities
{
    public class PostEntity : BaseEntity
    {
        public int ApplicationUserId { get; set; } 
        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual List<CommentEntity> Comments { get; set; }
        public virtual UserReactionEntity Reaction { get; set; }
        public string Text { get; set; }
        public string MediaUri { get; set; }
    }
}
