using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers;

public class UserReactionsController : BaseController<UserReactionEntity>
{
    public UserReactionsController(IDatabaseService<UserReactionEntity> databaseService, IMapper mapper) 
        : base(databaseService, mapper)
    {
    }
}