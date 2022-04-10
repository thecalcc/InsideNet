using System.ComponentModel.DataAnnotations;

namespace InsideNet.Data.Data.Models;

public class CommentDto : BaseDto
{
    [Required] public string Content { get; set; }

    public string? MediaUri { get; set; }

    [Required] public string ApplicationUserId { get; set; }

    [Required] public string PostId { get; set; }
}