using OnePageNet.App.Data.Models;
using OnePageNet.App.Models;

namespace OnePageNet.App.Data.Entities
{
    public class UserGroupEntity : BaseEntity
    {
        public virtual ICollection<GroupEntity> Group { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
