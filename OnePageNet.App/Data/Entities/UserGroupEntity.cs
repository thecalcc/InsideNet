using System.ComponentModel.DataAnnotations.Schema;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Data.Entities
{
    public class UserGroupEntity : BaseEntity
    {
        public virtual ICollection<GroupEntity> Group { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
