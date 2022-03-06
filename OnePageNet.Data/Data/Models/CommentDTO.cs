using System.ComponentModel.DataAnnotations;

namespace OnePageNet.Data.Data.Models;

public class CommentDto : BaseDTO
{
    [Required] public string Content { get; set; }

    [Required] public string MediaUri { get; set; }

    [Required] public string ApplicationUserId { get; set; }

    [Required] public string PostId { get; set; }
}