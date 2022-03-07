using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers;

public class MessagesController : BaseController<MessageEntity, MessageDto>
{
    public MessagesController(IDatabaseService<MessageEntity, MessageDto> databaseService)
        : base(databaseService)
    {
    }
}