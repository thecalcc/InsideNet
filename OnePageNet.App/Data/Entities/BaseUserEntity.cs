using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Data.Entities;

public class BaseUserEntity : BaseEntity
{
    public ApplicationUser ApplicationUser { get; set; }
}