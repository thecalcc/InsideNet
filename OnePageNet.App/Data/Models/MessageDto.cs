namespace OnePageNet.App.Data.Models;

public class MessageDto : BaseDTO
{
    public bool Read { get; set; }

    public bool Delivered { get; set; }

    // Relates to the ApplicationUser entity but we're sending only his ID so we don't transfer whole user objects back and forth
    public string SenderId { get; set; }

    // Relates to the GroupEntity but ...
    public string GroupDestinationId { get; set; }
    public string Content { get; set; }
    public string MediaUri { get; set; }
}