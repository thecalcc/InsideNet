using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers;

public class UserGroupsController : BaseController<UserGroupEntity>
{
    public UserGroupsController(IDatabaseService<UserGroupEntity> databaseService, IMapper mapper) 
        : base(databaseService, mapper)
    {
    }
}