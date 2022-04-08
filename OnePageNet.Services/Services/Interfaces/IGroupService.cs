using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePageNet.Services.Services.Interfaces
{
    public interface IGroupService: IDatabaseService<GroupEntity,GroupDTO>
    {
        Task AddAsync(GroupDTO dto, string creatorId, string targetId);
    }
}
