using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Entities;

public class CommentEntity : BaseEntity
{
    [Required] public string Content { get; set; }

    public string MediaUri { get; set; }
    
    [Required] public virtual ApplicationUser ApplicationUser { get; set; }

    [Required] public string PostId { get; set; }

    public virtual PostEntity Post { get; set; }
}