using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Data.Entities
{
    public class GroupEntity : BaseEntity
    {
        public virtual UserGroupEntity UserGroup { get; set; }
        public string MediaURI { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<MessageEntity> Message { get; set; } 
    }
}