namespace InsideNet.Data.Data.Entities;

public class UserGroupEntity : BaseEntity
{
    public virtual GroupEntity Group { get; set; }
    public virtual ApplicationUser User { get; set; }
}