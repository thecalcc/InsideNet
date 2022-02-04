namespace OnePageNet.App.Data.Entities
{
    public class GroupEntity : BaseEntity
    {
        public virtual UserGroupEntity UserGroup { get; set; }
        public string MediaUri { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MessageEntity> Message { get; set; } 
    }
}