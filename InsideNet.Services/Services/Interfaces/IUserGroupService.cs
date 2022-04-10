using InsideNet.Data.Data.Models;

namespace InsideNet.Services.Services.Interfaces
{
    public interface IUserGroupService
    {
        List<GroupDto> GetAllForUser(string userId);
        Task<UserGroupDto> GetById(string id);
        Task<bool> RemoveAsync(string id);
        Task<string> GetIdByComposite(string currentUserId, string groupId);
        Task<List<UserDto>> GetGroupParticipants(string groupId);
    }
}
