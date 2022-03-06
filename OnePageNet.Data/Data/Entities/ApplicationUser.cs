using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace OnePageNet.Data.Data.Entities;

public class ApplicationUser : IdentityUser
{
    [InverseProperty("CurrentUser")] public virtual ICollection<UserRelationEntity> CurrentRelationships { get; set; }
    [InverseProperty("TargetUser")] public virtual ICollection<UserRelationEntity> TargetRelationships { get; set; }
    public virtual ICollection<UserReactionEntity> Reactions { get; set; }
    public virtual ICollection<PostEntity> Posts { get; set; }
    public virtual ICollection<UserGroupEntity> Groups { get; set; }
    public string? MediaURI { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DoB { get; set; }
    public string? Gender { get; set; }
}