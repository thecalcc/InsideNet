using AutoMapper;
using Microsoft.EntityFrameworkCore;
using InsideNet.Data.Data;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;
using InsideNet.Services.Services.Interfaces;

namespace InsideNet.Services.Services
{
    public class GroupEntityService : DatabaseService<GroupEntity, GroupDto>, IGroupService
    {
        public GroupEntityService(OnePageNetDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<bool> AddAsync(GroupDto dto, string creatorId, string targetId)
        {
            var entity = Mapper.Map<GroupEntity>(dto);
            var groupEntities = await DbContext.GroupEntities.Where(x =>
                    (DbContext.UserGroupEntities.Any(y => (y.User.Id == creatorId && y.Group == x)) &&
                     DbContext.UserGroupEntities.Any(y => (y.User.Id == targetId && y.Group == x)) &&
                     DbContext.UserGroupEntities.Where(z => z.Group == x).ToList().Count == 2))
                .ToListAsync();

            if (!groupEntities.Any())
            {
                await DbContext.Set<GroupEntity>().AddAsync(entity);
                
                await DbContext.SaveChangesAsync();

                dto.Id = entity.Id;
                
                await AddUserGroup(dto.Id, creatorId);
                await AddUserGroup(dto.Id, targetId);

                await DbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        private async Task AddUserGroup(string groupId, string userId)
        {
            var user = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == userId) ??
                       throw new Exception("Current user does not exist.");
            var group = await DbContext.GroupEntities.FirstOrDefaultAsync(x => x.Id == groupId) ??
                        throw new Exception("Current group does not exist.");


            var userGroup = new UserGroupEntity
            {
                User = user,
                Group = group
            };

            await DbContext.UserGroupEntities.AddAsync(userGroup);
        }
    }
}