using OnePageNet.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePageNet.Services.Services.Interfaces
{
    public interface IUserGroupService
    {
        Task<List<UserGroupDTO>> GetAll(string userId);
        Task AddAsync(string groupId, string userId);
        Task<UserGroupDTO> GetById(string id);
        Task<bool> DeleteAsync(UserGroupDTO dto);
        Task<string> GetIdByComposite(string currentUserId, string GroupId);
    }
}
