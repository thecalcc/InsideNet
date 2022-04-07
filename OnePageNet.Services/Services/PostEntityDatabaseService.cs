using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.Services.Services;

public class PostEntityDatabaseService : DatabaseService<PostEntity, PostDto>, IPostService
{
    private readonly OnePageNetDbContext _dbContext;

    public PostEntityDatabaseService(OnePageNetDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AttachUser(PostDto postDto)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == postDto.PosterId);

        if (user?.Id != postDto.PosterId) return false;

        _dbContext.Attach(user);

        return true;
    }
    public async Task<List<PostDto>> GetTimeline(string id)
    {
        var posts = await _dbContext.PostEntities.ToListAsync();
        var userRelations = await _dbContext.UserRelationEntities.Include(x => x.CurrentUser)
                .Include(x => x.TargetUser)
                .Include(x => x.UserRelationship).ToListAsync();
        var timeline = posts.Where(x => (userRelations.Where(y => (y.TargetUser.Id == x.PosterId) && (y.CurrentUser.Id == id) && (y.UserRelationship.Name == Helpers.Helpers.UserRelationConstants.Friends)).Any() || x.PosterId == id)).ToList();
        return _mapper.Map<List<PostDto>>(timeline);
    }
}