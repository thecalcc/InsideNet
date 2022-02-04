using AutoMapper;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers;

public class UserRelationsController : BaseController<UserRelationEntity, UserRelationDto>
{
    public UserRelationsController(IDatabaseService<UserRelationEntity> databaseService, IMapper mapper) 
        : base(databaseService, mapper)
    {
    }
}