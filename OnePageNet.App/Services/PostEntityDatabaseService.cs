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

    public async Task<bool>  AttachUser(string id, PostEntity? entity = null)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
        
        if (user?.Id != id) return false;
        
        entity.Poster = user;
        return true;
    }
    
    // public Task<IEnumerable<PostEntity>> ToListAsync()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task<PostEntity> FindByPublicId(string publicId)
    // {
    //     throw new NotImplementedException();
    // }
    //
    //
    //
    // public void Update(PostEntity entity)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task SaveChangesAsync()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task AddAsync(PostEntity entity)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public void Remove(PostEntity entity)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public bool Exists(string publicId)
    // {
    //     throw new NotImplementedException();
    // }
}