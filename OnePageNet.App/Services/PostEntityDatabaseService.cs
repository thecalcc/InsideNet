using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Services;

public class PostEntityDatabaseService : DatabaseService<PostDto, PostEntity>, IDatabaseService<PostDto, PostEntity>
{
    private readonly OnePageNetDbContext _dbContext;

    public PostEntityDatabaseService(OnePageNetDbContext dbContext, Mapper mapper)
        : base(dbContext, mapper)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AttachUser(PostEntity entity)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == entity.PosterId);

        if (user?.Id != entity.PosterId) return false;

        _dbContext.Attach(user);

        return true;
    }
}