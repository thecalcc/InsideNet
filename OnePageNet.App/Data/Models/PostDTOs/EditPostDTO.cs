using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models.PostDTOs
{
    public class EditPostDto
    {
        [Required]
        public string PublicId { get; set; }
        [Required]
        public string UserId { get; set; }
        public string Text { get; set; }
        public string MediaUri { get; set; }
    }
}
