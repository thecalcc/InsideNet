using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePageNet.Services.Services
{
    public class GroupService : DatabaseService<GroupEntity, GroupDTO>, IGroupService
    {
        public GroupService(OnePageNetDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task AddAsync(GroupDTO dto, string creatorId, string targetId)
        {
            var entity = _mapper.Map<GroupEntity>(dto);
            var test = await _dbContext.GroupEntities.Where(x => ( _dbContext.UserGroupEntities.Where(y => (y.Users.Id == creatorId && y.Group == x)).Any() && _dbContext.UserGroupEntities.Where(y => (y.Users.Id == targetId && y.Group == x)).Any() && _dbContext.UserGroupEntities.Where(z => z.Group == x).ToList().Count == 2)).ToListAsync();

            if (test.Count() < 1)
            {
                await _dbContext.Set<GroupEntity>().AddAsync(entity);

                await _dbContext.SaveChangesAsync();

                dto.Id = entity.Id;
            }

        }
    }
}
