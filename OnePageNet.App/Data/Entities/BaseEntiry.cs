using System.ComponentModel.DataAnnotations;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Data.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public string? PublicId { get; set; }

        [Required]
        public DateTime DeletedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}