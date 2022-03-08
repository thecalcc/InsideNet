using Microsoft.AspNetCore.SignalR;
using OnePageNet.Data.Data.Models;

namespace OnePageNet.App.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessage(MessageDto message)
    {
        await Clients.All.ReceiveMessage(message);
    }
}