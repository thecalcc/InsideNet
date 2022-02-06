using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Services
{
    public class MessageEntityDatabaseService:DatabaseService<MessageEntity>, IDatabaseService<MessageEntity>
    {
        private readonly OnePageNetDbContext _dbContext;

        public MessageEntityDatabaseService(OnePageNetDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<bool> AttachUser(MessageEntity entity)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == entity.SenderId);
            var group = await _dbContext.GroupEntities.SingleOrDefaultAsync(x => x.Id == entity.DestinationId);

            if (user?.Id != entity.SenderId) return false;

            _dbContext.Attach(user);

            _dbContext.Attach(group);

            return true;
        }
    }
}
