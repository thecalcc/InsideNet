namespace OnePageNet.App.Data.Entities;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
    public DateTime DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}