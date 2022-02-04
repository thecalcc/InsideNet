using System.ComponentModel.DataAnnotations;
using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Data.Models
{
    public class CommentDto : BaseDto
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string MediaUri { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public virtual PostEntity Post { get; set; }
    }
}
