namespace OnePageNet.App.Data.Models
{
    public class BaseDto
    {
        public BaseDto()
        {
            PublicId = Guid.NewGuid().ToString();
        }

        public string PublicId { get; set; }

        public DateTime DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
