using AutoMapper;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers;

public class UserGroupsController : BaseController<UserGroupEntity, UserGroupDto>
{
    public UserGroupsController(IDatabaseService<UserGroupEntity> databaseService, IMapper mapper) 
        : base(databaseService, mapper)
    {
    }
}