using System.ComponentModel.DataAnnotations;

namespace InsideNet.Data.Data.Models;

public class PostDto : BaseDto
{
    [Required] public string PosterId { get; set; }
    [Required] public string Title { get; set; }
    public List<string>? CommentsIds { get; set; }
    public string? ReactionId { get; set; }
    public string Text { get; set; }
    public string? MediaUri { get; set; }
}