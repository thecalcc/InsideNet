using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnePageNet.App.Data.Entities;

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

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Deleted)
            {
                ((BaseEntity)entityEntry.Entity).DeletedAt = DateTime.Now;
            }
            
            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}