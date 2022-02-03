using Microsoft.AspNetCore.Identity;
using OnePageNet.App.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnePageNet.App.Data.Models;

public class ApplicationUser : IdentityUser
{
    [InverseProperty("CurrentUser")]
    public virtual ICollection<UserRelationEntity> CurrentRelationships { get; set; }
    [InverseProperty("TargetUser")]
    public virtual ICollection<UserRelationEntity> TargetRelationships { get; set; }
    public virtual ICollection<UserReactionEntity> Reactions { get; set; }
    public virtual ICollection<PostEntity> Posts { get; set; }
    public virtual ICollection<UserGroupEntity> Groups { get; set; }
}