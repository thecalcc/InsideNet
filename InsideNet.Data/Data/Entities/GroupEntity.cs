using System.ComponentModel.DataAnnotations;

namespace InsideNet.Data.Data.Entities;

public class GroupEntity : BaseEntity
{
    public virtual ICollection<UserGroupEntity> UserGroup { get; set; }
    public string? MediaUri { get; set; }
    [Required] public string Name { get; set; }
    public virtual ICollection<MessageEntity>? Message { get; set; }
}