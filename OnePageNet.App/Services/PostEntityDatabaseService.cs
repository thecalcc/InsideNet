using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Services;

public class PostEntityDatabaseService : DatabaseService<PostEntity>, IDatabaseService<PostEntity>
{
    private readonly OnePageNetDbContext _dbContext;

    public PostEntityDatabaseService(OnePageNetDbContext dbContext) : base(dbContext)
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