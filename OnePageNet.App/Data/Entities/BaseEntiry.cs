using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public DateTime DeletedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}