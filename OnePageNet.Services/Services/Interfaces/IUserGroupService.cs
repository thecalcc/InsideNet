using OnePageNet.Data.Data.Models;

namespace OnePageNet.Services.Services.Interfaces
{
    public interface IUserGroupService
    {
        List<GroupDTO> GetAllForUser(string userId);
        Task<UserGroupDTO> GetById(string id);
        Task<bool> RemoveAsync(string id);
        Task<string> GetIdByComposite(string currentUserId, string groupId);
        Task<List<UserDto>> GetGroupParticipants(string groupId);
    }
}
