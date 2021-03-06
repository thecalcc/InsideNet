using AutoMapper;
using Microsoft.EntityFrameworkCore;
using InsideNet.Data.Data;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;
using InsideNet.Services.Services.Interfaces;

namespace InsideNet.Services.Services
{
    public class UserGroupEntityService : IUserGroupService
    {
        private readonly OnePageNetDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserGroupEntityService(OnePageNetDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        

        public async Task<bool> RemoveAsync(string id)
        {
            var userGroup = await _dbContext.UserGroupEntities.FirstOrDefaultAsync(x => x.Id == id);
            var group = userGroup?.Group;
            _dbContext.UserGroupEntities.Remove(
                await _dbContext.UserGroupEntities.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new Exception("No such relation found"));
            var groupMembers = await _dbContext.UserGroupEntities.Where(x => x.Group == group).ToListAsync();
            if (groupMembers.Count() <= 2)
            {
                _dbContext.GroupEntities.Remove(group);
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public List<GroupDto> GetAllForUser(string userId)
        {
            var dtos = _mapper.Map<List<GroupDto>>(_dbContext.UserGroupEntities
                .Where(x => x.User.Id == userId)
                .Include(x => x.Group)
                .Include(x => x.User)
                .Select(x => x.Group));

            return dtos ??
                   throw new Exception("No userGroups found (you are alone)");
        }

        public async Task<UserGroupDto> GetById(string id)
        {
            return _mapper.Map<UserGroupDto>(await _dbContext.UserGroupEntities
                .Include(x => x.User)
                .Include(x => x.Group)
                .FirstOrDefaultAsync(x => x.Id == id)) ?? throw new Exception("No such relation found");
        }

        public async Task<string> GetIdByComposite(string currentUserId, string groupId)
        {
            var userGroup = await _dbContext.UserGroupEntities
                .Include(x => x.User)
                .Include(x => x.Group)
                .FirstOrDefaultAsync(x => x.User.Id == currentUserId && x.Group.Id == groupId);

            return userGroup?.Id ??
                   throw new Exception("There's no such entity");
        }

        public async Task<List<UserDto>> GetGroupParticipants(string groupId)
        {
            var userGroups = await _dbContext.UserGroupEntities.Where(userGroup => userGroup.Group.Id == groupId).Include(x => x.Group).Include(x => x.User).ToListAsync();
            var users = _mapper.Map<List<UserDto>>(userGroups.Select(x => x.User).ToList());
            return users;
        }
    }
}