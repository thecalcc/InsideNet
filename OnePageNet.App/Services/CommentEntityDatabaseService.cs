using AutoMapper;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
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

        public CommentEntityDatabaseService(OnePageNetDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AttachUser(CommentDto commentDto)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == commentDto.ApplicationUserId);
            var post = await _dbContext.PostEntities.SingleOrDefaultAsync(x => x.Id == commentDto.PostId);

            if (user == null || post == null) return false;

            _dbContext.Attach(user);
            _dbContext.Attach(post);

            return true;
        }
    }
}