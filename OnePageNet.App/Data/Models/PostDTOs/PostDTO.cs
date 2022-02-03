using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models.PostDTOs
{
    public class PostDto : BaseDto
    {
        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual List<CommentDto> Comments { get; set; }
        public UserReactionDto Reaction { get; set; }
        public string Text { get; set; }
        public string MediaUri { get; set; }
    }
}
