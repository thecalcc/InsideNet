using AutoMapper;
using Microsoft.EntityFrameworkCore;
using InsideNet.Data.Data;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;
using InsideNet.Services.Services.Interfaces;

namespace InsideNet.Services.Services
{
    public class MessageEntityService : DatabaseService<MessageEntity, MessageDto>,
        IMessageEntityDatabaseService
    {
        private readonly OnePageNetDbContext _dbContext;
        private readonly IMapper _mapper;

        public MessageEntityService(OnePageNetDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<MessageDto>> GetAllById(string groupId)
        {
            var messages =
                _mapper.Map<List<MessageDto>>(_dbContext.MessageEntities.Where(x => x.DestinationId == groupId));

            return messages;
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