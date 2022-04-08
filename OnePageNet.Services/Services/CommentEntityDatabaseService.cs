using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.Services.Services
{
    public class CommentEntityDatabaseService : DatabaseService<CommentEntity, CommentDto>,
        ICommentService
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

        public async Task<List<CommentDto>> GetAllById(string id)
        {
            var comments = await _dbContext.CommentEntities
                .Where(x => x.PostId == id)
                .Include(x => x.ApplicationUser)
                .Include(x => x.Post)
                .ToListAsync();
            
            return _mapper.Map<List<CommentDto>>(comments);
        }
    }
}