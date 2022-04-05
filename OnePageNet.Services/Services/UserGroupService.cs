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
                Users = user,
                Group = group
            };
            

            await _dbContext.UserGroupEntities.AddAsync(userGroup);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<bool> DeleteAsync(UserGroupDTO dto)
        {
            _dbContext.UserGroupEntities.Remove(_mapper.Map<UserGroupEntity>(dto));
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserGroupDTO>> GetAll(string userId)
        {
            var dtos = _mapper.Map<List<UserGroupDTO>>(await _dbContext.UserGroupEntities
               .Where(x => x.Users.Id == userId)
               .Include(x => x.Group)
               .Include(x => x.Users)
               .ToListAsync());

            return dtos ??
                   throw new Exception("No userGroups found (you are alone)");
        }

        public async Task<UserGroupDTO> GetById(string id)
        {
            return _mapper.Map<UserGroupDTO>(await _dbContext.UserGroupEntities.Include("Users")
                       .Include("Group").FirstOrDefaultAsync(x =>
                           x.Id == id)) ??
                   throw new Exception("No such relation found");
        }

        public async Task<string> GetIdByComposite(string currentUserId, string GroupId) 
        {
            var userGroup = await _dbContext.UserGroupEntities.Include("Users")
                       .Include("Group").FirstOrDefaultAsync(x =>
                           x.Users.Id == currentUserId && x.Group.Id == GroupId);
            return userGroup.Id ??
                   throw new Exception("There's no such entity");
        }

      
    }
}
