using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models;

public class PostDto : BaseDTO
{
    [Required] public string PosterId { get; set; }
    public List<string>? CommentsIds { get; set; }
    public string? ReactionId { get; set; }
    public string Text { get; set; }
    public string? MediaUri { get; set; }
}