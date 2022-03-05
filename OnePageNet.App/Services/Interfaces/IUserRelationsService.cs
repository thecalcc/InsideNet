using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services.Interfaces
{
    public interface IUserRelationsService
    {
        Task<List<UserRelationsDto>> GetAll(string userId);
        Task AddAsync(string currUserId, string targetUserId);
        Task<UserRelationsDto> GetById(string currentUserId, string targetUserId);
        Task<bool> Update(string currentUserId, string targetUserId, string command);
    }
}