using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models.PostDTOs
{
    public class PostDTO : BaseDTO
    {
        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual List<CommentDTO> Comments { get; set; }
        public UserReactionDTO Reaction { get; set; }
        // TODO Add last two fields to DB
        public string Text { get; set; }
        public string MediaURI { get; set; }

    }
}
