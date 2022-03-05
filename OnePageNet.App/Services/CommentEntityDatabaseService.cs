using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.AutoMapper;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Services
{
    public class CommentEntityDatabaseService : DatabaseService<CommentDto, CommentEntity>,
        IDatabaseService<CommentDto, CommentEntity>
    {
        private readonly OnePageNetDbContext _dbContext;

        public CommentEntityDatabaseService(OnePageNetDbContext dbContext) : base(dbContext,
            new Mapper(new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); })))
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AttachUser(CommentDto entity)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == entity.ApplicationUserId);
            var post = await _dbContext.PostEntities.SingleOrDefaultAsync(x => x.Id == entity.PostId);

            if (user?.Id != entity.ApplicationUserId) return false;

            _dbContext.Attach(user);
            _dbContext.Attach(post);

            return true;
        }
    }
}