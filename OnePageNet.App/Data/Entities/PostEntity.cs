using OnePageNet.App.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models
{
    public class PostEntity : BaseEntity
    {
        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual List<CommentEntity> Comments { get; set; }
        public UserReactionEntity Reaction { get; set; }
        // TODO Add last two fields to DB
        public string Text { get; set; }
        public string MediaURI { get; set; }
    }
}
