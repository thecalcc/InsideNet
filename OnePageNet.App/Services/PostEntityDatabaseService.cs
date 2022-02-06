using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;

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
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == entity.ApplicationUserId);
    
        if (user?.Id != entity.ApplicationUserId) return false;
    
        _dbContext.Attach(user);
    
        return true;
    }
}