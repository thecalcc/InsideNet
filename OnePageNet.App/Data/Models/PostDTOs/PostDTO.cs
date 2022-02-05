using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models.PostDTOs
{
    public class PostDto : BaseDto
    {
        public List<string> CommentsIds { get; set; }
        public string ReactionId { get; set; }
        public string Text { get; set; }
        public string MediaUri { get; set; }
    }
}
