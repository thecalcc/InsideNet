using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using OnePageNet.App.Data.Enums;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Data.Entities
{
    public class PostEntity : BaseEntity
    {
        public string Text { get; set; }
        public string MediaUri { get; set; }
        public string PosterId { get; set; }
        public virtual ApplicationUser Poster { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; set; }
        public virtual ICollection<ReactionEntity> Reaction { get; set; }
    }
}