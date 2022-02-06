using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Entities;

public class MessageEntity : BaseEntity
{
    public bool Read { get; set; }
    public bool Delivered { get; set; }

    [Required] public virtual ApplicationUser Sender { get; set; }

    [Required] public virtual GroupEntity Destination { get; set; }

    [Required] public string Content { get; set; }

    public string MediaUri { get; set; }
}