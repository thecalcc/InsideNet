using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Entities;

public class PostEntity : BaseEntity
{
    public string Text { get; set; }
    public string? MediaUri { get; set; }
    [Required] public string PosterId { get; set; }
    public virtual ApplicationUser Poster { get; set; }
    public virtual ICollection<CommentEntity>? Comments { get; set; }
    public virtual ICollection<ReactionEntity>? Reaction { get; set; }
}