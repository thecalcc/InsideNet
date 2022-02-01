using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Models;

namespace OnePageNet.App.Data;

public class OnePageNetDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public OnePageNetDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {

    }

    public DbSet<CommentEntity> CommentEntities { get; set; }
    public DbSet<GroupEntity> GroupEntities { get; set; }
    public DbSet<MessageEntity> MessageEntities { get; set; }
    public DbSet<PostEntity> PostEntities { get; set; }
    public DbSet<UserGroupEntity> UserGroupEntities { get; set; }
    public DbSet<UserReactionEntity> UserReactionEntities { get; set; }
    public DbSet<UserRelationEntity> UserRelationEntities { get; set; }
}