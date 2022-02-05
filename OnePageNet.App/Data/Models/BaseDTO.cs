namespace OnePageNet.App.Data.Models
{
    public class BaseDto
    {
        public string? ApplicationUserId { get; set; }
        public string PublicId { get; set; }

        public DateTime DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
