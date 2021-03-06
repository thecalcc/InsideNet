using AutoMapper;
using Microsoft.EntityFrameworkCore;
using InsideNet.Data.Data;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;
using InsideNet.Services.Services.Interfaces;

namespace InsideNet.Services.Services;

public class PostEntityService : DatabaseService<PostEntity, PostDto>, IPostService
{
    private readonly OnePageNetDbContext _dbContext;
    private readonly IMapper _mapper;

    public PostEntityService(OnePageNetDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
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
        var timeline = posts.Where(x => (userRelations.Where(y => (y.TargetUser.Id == x.PosterId) && (y.CurrentUser.Id == id) && (y.UserRelationship.Name == InsideNet.Helpers.Helpers.UserRelationConstants.Friends)).Any() || x.PosterId == id)).ToList();
        return Mapper.Map<List<PostDto>>(timeline);
    }

    public async Task<List<PostDto>> GetPostsForUserById(string posterId)
    {
        var posts = await _dbContext.PostEntities.Where(x => x.PosterId == posterId).ToListAsync();
        return _mapper.Map<List<PostDto>>(posts);
    }
}