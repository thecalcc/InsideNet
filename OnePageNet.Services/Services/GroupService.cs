﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

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
            var groupEntities = await _dbContext.GroupEntities.Where(x =>
                    (_dbContext.UserGroupEntities.Any(y => (y.User.Id == creatorId && y.Group == x)) &&
                     _dbContext.UserGroupEntities.Any(y => (y.User.Id == targetId && y.Group == x)) &&
                     _dbContext.UserGroupEntities.Where(z => z.Group == x).ToList().Count == 2))
                .ToListAsync();

            if (!groupEntities.Any())
            {
                await _dbContext.Set<GroupEntity>().AddAsync(entity);

                await _dbContext.SaveChangesAsync();

                dto.Id = entity.Id;
            }
        }
    }
}