using System.ComponentModel.DataAnnotations;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Data.Entities
{
    public class CommentEntity : BaseEntity
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string MediaUri { get; set; }

        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public virtual PostEntity Post { get; set; }
    }
}