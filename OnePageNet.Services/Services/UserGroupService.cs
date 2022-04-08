using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.Services.Services
{
    public class UserGroupService : IUserGroupService
    {
        private readonly OnePageNetDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserGroupService(OnePageNetDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(string groupId, string userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId) ??
                       throw new Exception("Current user does not exist.");
            var group = await _dbContext.GroupEntities.FirstOrDefaultAsync(x => x.Id == groupId) ??
                        throw new Exception("Current group does not exist.");


            var userGroup = new UserGroupEntity
            {
                User = user,
                Group = group
            };

            await _dbContext.UserGroupEntities.AddAsync(userGroup);
            await _dbContext.SaveChangesAsync();
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

        public List<GroupDTO> GetAllForUser(string userId)
        {
            var dtos = _mapper.Map<List<GroupDTO>>(_dbContext.UserGroupEntities
                .Where(x => x.User.Id == userId)
                .Include(x => x.Group)
                .Include(x => x.User)
                .Select(x => x.Group));

            return dtos ??
                   throw new Exception("No userGroups found (you are alone)");
        }

        public async Task<UserGroupDTO> GetById(string id)
        {
            return _mapper.Map<UserGroupDTO>(await _dbContext.UserGroupEntities
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
    }
}