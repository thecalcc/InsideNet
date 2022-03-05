﻿using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateDateColumns();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateDateColumns();
        return base.SaveChanges();
    }

    private void UpdateDateColumns()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in ChangeTracker.Entries()
                     .Where(x => x.Entity is IdentityUser && x.State is EntityState.Added))
        {
            ((IdentityUser) entityEntry.Entity).Id = Guid.NewGuid().ToString();
        }

        foreach (var entityEntry in entries)
        {
            switch (entityEntry.State)
            {
                case EntityState.Deleted:
                    ((BaseEntity) entityEntry.Entity).DeletedAt = DateTime.Now;
                    break;
                case EntityState.Added:
                    ((BaseEntity) entityEntry.Entity).CreatedAt = DateTime.Now;
                    break;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}