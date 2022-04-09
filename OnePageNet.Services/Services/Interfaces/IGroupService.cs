﻿using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;

namespace OnePageNet.Services.Services.Interfaces
{
    public interface IGroupService: IDatabaseService<GroupEntity,GroupDTO>
    {
        Task AddAsync(GroupDTO dto, string creatorId, string targetId);
    }
}