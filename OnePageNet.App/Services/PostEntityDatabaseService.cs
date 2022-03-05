using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.AutoMapper;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services;

public class PostEntityDatabaseService : DatabaseService<PostDto, PostEntity>
{
    private readonly OnePageNetDbContext _dbContext;

    public PostEntityDatabaseService(OnePageNetDbContext dbContext, IMapper mapper)
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