using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models.PostDTOs
{
    public class EditPostDTO
    {
        [Required]
        public string PublicId { get; set; }
        [Required]
        public ApplicationUser ApplicationUser { get; set; }
        public string Text { get; set; }
        public string MediaURI { get; set; }
    }
}
