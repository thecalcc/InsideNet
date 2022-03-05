using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Services
{
    public class UserRelationsService : IUserRelationsService
    {
        private readonly OnePageNetDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserService userService;

        public UserRelationsService(OnePageNetDbContext dbContext, IMapper mapper, IUserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            this.userService = userService;
        }

        public async Task<List<UserRelationsDto>> GetAll(string userId)
        {
            var dtos = _mapper.Map<List<UserRelationsDto>>(await _dbContext.UserRelationEntities.Where(x => x.CurrentUser.Id == userId).Include("CurrentUser").Include("TargetUser").Include("UserRelationship").ToListAsync());
            return dtos ??
                   throw new Exception("No userRelations found");
        }
        public async Task<UserRelationsDto> GetById(string currentUserId, string targetUserId)
        {
            var dto = _mapper.Map<UserRelationsDto>(await _dbContext.UserRelationEntities.Include("CurrentUser").Include("TargetUser").Include("UserRelationship").FirstOrDefaultAsync(x => x.CurrentUser.Id == currentUserId && x.TargetUser.Id == targetUserId));
            return dto ??
                   throw new Exception("No such relation found");
        }

        public async Task AddAsync(string currUserId, string targetUserId, string relationId)
        {
            var currUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == currUserId);
            var targetUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == targetUserId);
            var relation = await _dbContext.RelationEntities.FirstOrDefaultAsync(x => x.Id == relationId);

            if (currUser == null) throw new Exception("Current user does not exist.");
            if (targetUser == null) throw new Exception("Target user does not exist.");
            if (relation == null) throw new Exception("Relation does not exist.");


            UserRelationEntity entity = new UserRelationEntity
            {
                CurrentUser = currUser,
                TargetUser = targetUser,
                UserRelationship = relation

            };

            await _dbContext.Set<UserRelationEntity>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

    }
}
