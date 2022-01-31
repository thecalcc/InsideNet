using OnePageNet.App.Data.Enums;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnePageNet.App.Data.Entities
{
    public class UserRelationEntity : BaseEntity
    {
        public UserRelation UserRelationship { get; set; }
        public ICollection<ApplicationUser> CurrentUser { get; set; }
        public ICollection<ApplicationUser> TargetUser { get; set; }
    }
}
