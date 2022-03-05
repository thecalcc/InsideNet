using AutoMapper;
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

        public MessageEntityDatabaseService(OnePageNetDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AttachUser(MessageDto messageDto)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == messageDto.SenderId);
            var group = await _dbContext.GroupEntities.SingleOrDefaultAsync(x => x.Id == messageDto.DestinationId);

            if (user?.Id != messageDto.SenderId) return false;

            _dbContext.Attach(user);
            if (group == null) return false;
            _dbContext.Attach(group);

            return true;
        }
    }
}