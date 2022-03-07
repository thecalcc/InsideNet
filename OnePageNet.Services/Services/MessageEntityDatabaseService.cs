using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.Services.Services
{
    public class MessageEntityDatabaseService : DatabaseService<MessageEntity, MessageDto>,
        IDatabaseService<MessageEntity, MessageDto>
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