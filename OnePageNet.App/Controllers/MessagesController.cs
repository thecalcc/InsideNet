using AutoMapper;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Controllers;

public class MessagesController : BaseController<MessageEntity, MessageDto>
{
    public MessagesController(IDatabaseService<MessageDto, MessageEntity> databaseService, IMapper mapper)
        : base(databaseService, mapper)
    {
    }
}