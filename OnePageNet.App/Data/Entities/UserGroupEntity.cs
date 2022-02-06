namespace OnePageNet.App.Data.Entities;

public class UserGroupEntity : BaseEntity
{
    public virtual GroupEntity Group { get; set; }
    public virtual ApplicationUser Users { get; set; }
}