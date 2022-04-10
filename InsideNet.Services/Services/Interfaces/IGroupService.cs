using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;

namespace InsideNet.Services.Services.Interfaces
{
    public interface IGroupService: IDatabaseService<GroupEntity,GroupDto>
    {
        Task<bool> AddAsync(GroupDto dto, string creatorId, string targetId);
    }
}
