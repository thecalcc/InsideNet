using AutoMapper;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers;

public class MessagesController : BaseController<MessageEntity>
{
    public MessagesController(IDatabaseService<MessageEntity> databaseService, IMapper mapper) 
        : base(databaseService, mapper)
    {
    }
}