using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services.Interfaces
{
    public interface IUserRelationsService
    {
        Task<List<UserRelationsDto>> GetAll(string userId);
        Task AddAsync(string currUserId, string targetUserId, string relationId);
        Task<UserRelationsDto> GetById(string currentUserId, string targetUserId);
    }
}
