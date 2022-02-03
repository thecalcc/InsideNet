using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Data.Entities
{
    public class MessageEntity : BaseEntity
    {
        public bool Read { get; set; }
        public bool Delivered { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public virtual GroupEntity Destination { get; set; }
        public string Content { get; set; }
        public string MediaURI { get; set; }
    }
}
