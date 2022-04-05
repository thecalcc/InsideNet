using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.Services.Services;

public interface IMessageEntityDatabaseService : IDatabaseService<MessageEntity, MessageDto>
{
}