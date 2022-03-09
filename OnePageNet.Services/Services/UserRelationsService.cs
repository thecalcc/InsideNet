using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Helpers.Helpers;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.Services.Services
{
    public class UserRelationsService : IUserRelationsService
    {
        private readonly OnePageNetDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRelationsService(OnePageNetDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserRelationsDto>> GetAll(string userId)
        {
            var dtos = _mapper.Map<List<UserRelationsDto>>(await _dbContext.UserRelationEntities
                .Where(x => x.CurrentUser.Id == userId)
                .Include("CurrentUser").Include("TargetUser")
                .Include("UserRelationship").ToListAsync());
            return dtos ??
                   throw new Exception("No userRelations found");
        }

        public async Task<UserRelationsDto> GetById(string currentUserId, string targetUserId)
        {
            return _mapper.Map<UserRelationsDto>(await GetByCompositeIds(currentUserId, targetUserId)) ??
                   throw new Exception("No such relation found");
        }

        public async Task AddAsync(string currUserId, string targetUserId)
        {
            var currUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == currUserId) ??
                           throw new Exception("Current user does not exist.");
            var targetUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == targetUserId) ??
                             throw new Exception("Target user does not exist.");

            var relationAccept =
                await _dbContext.RelationEntities.FirstOrDefaultAsync(x =>
                    x.Name == UserRelationConstants.AcceptInvite) ??
                throw new Exception("Relation does not exist.");

            var relationPending =
                await _dbContext.RelationEntities.FirstOrDefaultAsync(
                    x => x.Name == UserRelationConstants.PendingInvite) ??
                throw new Exception("Relation does not exist.");

            var pending = new UserRelationEntity
            {
                CurrentUser = currUser,
                TargetUser = targetUser,
                UserRelationship = relationPending
            };

            var accepting = new UserRelationEntity
            {
                CurrentUser = targetUser,
                TargetUser = currUser,
                UserRelationship = relationAccept
            };

            await _dbContext.Set<UserRelationEntity>()
                .AddRangeAsync(new List<UserRelationEntity> {pending, accepting});

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Update(string currentUserId, string targetUserId, string command)
        {
            var entity = await GetByCompositeIds(currentUserId, targetUserId);

            switch (command)
            {
                case UserRelationConstants.PendingInvite:
                {
                    _dbContext.UserRelationEntities.Remove(entity);
                    return true;
                }
                case UserRelationConstants.AcceptInvite:
                {
                    entity.UserRelationship = await FindRelationByName(UserRelationConstants.Friends);
                    break;
                }
                default:
                {
                    await AddAsync(currentUserId, targetUserId);
                    return true;
                }
            }

            return false;
        }

        private async Task<UserRelationEntity> GetByCompositeIds(string currentUserId, string targetUserId)
        {
            return await _dbContext.UserRelationEntities.Include("CurrentUser")
                       .Include("TargetUser").Include("UserRelationship").FirstOrDefaultAsync(x =>
                           x.CurrentUser.Id == currentUserId && x.TargetUser.Id == targetUserId) ??
                   throw new Exception("There's no such entity");
        }

        private async Task<RelationEntity> FindRelationByName(string name)
        {
            return await _dbContext.RelationEntities.FirstOrDefaultAsync(x => x.Name == name) ??
                   throw new Exception("There's no such relation with the given name");
        }
    }
}