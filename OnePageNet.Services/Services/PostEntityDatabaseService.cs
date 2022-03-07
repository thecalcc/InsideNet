using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;

namespace OnePageNet.Services.Services;

public class PostEntityDatabaseService : DatabaseService<PostEntity, PostDto>
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
}