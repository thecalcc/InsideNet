using AutoMapper;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Services
{
    public class MessageEntityDatabaseService : DatabaseService<MessageDto, MessageEntity>, IDatabaseService<MessageDto, MessageEntity>
    {
        private readonly OnePageNetDbContext _dbContext;

        public MessageEntityDatabaseService(OnePageNetDbContext dbContext, Mapper mapper)
            : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AttachUser(MessageDto entity)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == entity.SenderId);
            var group = await _dbContext.GroupEntities.SingleOrDefaultAsync(x => x.Id == entity.DestinationId);

            if (user?.Id != entity.SenderId) return false;

            _dbContext.Attach(user);
            if (group == null) return false;
            _dbContext.Attach(group);

            return true;
        }
    }
}