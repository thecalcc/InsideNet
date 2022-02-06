using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers;

public class UserRelationsController : BaseController<UserRelationEntity>
{
    public UserRelationsController(IDatabaseService<UserRelationEntity> databaseService, IMapper mapper) 
        : base(databaseService, mapper)
    {
    }
}