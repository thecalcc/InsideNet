using InsideNet.Data.Data.Models;

namespace InsideNet.Services.Services.Interfaces
{
    public interface IUserRelationsService
    {
        Task<List<UserRelationDto>> GetAll(string userId);
        Task AddAsync(string currUserId, string targetUserId);
        Task<UserRelationDto> GetById(string currentUserId, string targetUserId);
        Task<bool> Update(UpdateUserRelationsDto dto);
    }
}