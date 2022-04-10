using System.ComponentModel.DataAnnotations;

namespace InsideNet.Data.Data.Entities;

public class CommentEntity : BaseEntity
{
    [Required] public string Content { get; set; }
    public string? MediaUri { get; set; }
    [Required] public string ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
    [Required] public string PostId { get; set; }
    public virtual PostEntity Post { get; set; }
}