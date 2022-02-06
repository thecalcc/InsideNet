using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Services
{
    public class CommentEntityDatabaseService : DatabaseService<CommentEntity>, IDatabaseService<CommentEntity>
    {
        private readonly OnePageNetDbContext _dbContext;

        public CommentEntityDatabaseService(OnePageNetDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<bool> AttachUser(CommentEntity entity)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == entity.ApplicationUserId);

            if (user?.Id != entity.ApplicationUserId) return false;

            _dbContext.Attach(user);

            return true;
        }
    }
}
