using AutoMapper;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers;

public class UserReactionsController : BaseController<UserReactionEntity, UserReactionDto>
{
    public UserReactionsController(IDatabaseService<UserReactionEntity> databaseService, IMapper mapper) 
        : base(databaseService, mapper)
    {
    }
}