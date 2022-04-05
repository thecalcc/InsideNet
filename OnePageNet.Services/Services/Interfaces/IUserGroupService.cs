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
        Task AddAsync(string currUserId, string targetUserId);
        Task<UserGroupDTO> GetById(string currentUserId, string targetUserId);
        Task<bool> Delete(string id);
    }
}
