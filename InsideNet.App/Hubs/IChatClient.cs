using InsideNet.Data.Data.Models;

namespace InsideNet.App.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(MessageDto messageDto);
}