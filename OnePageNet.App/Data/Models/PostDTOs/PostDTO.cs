using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models.PostDTOs
{
    public class PostDto : BaseDto
    {
        [Required]
        public string UserId { get; set; }
        public List<string> CommentsIds { get; set; }
        public string ReactionId { get; set; }
        public string Text { get; set; }
        public string MediaUri { get; set; }
    }
}
