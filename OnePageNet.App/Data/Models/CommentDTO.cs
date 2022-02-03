using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models
{
    public class CommentDTO : BaseDTO
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string MediaURI { get; set; }

        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public virtual PostEntity Post { get; set; }
    }
}
