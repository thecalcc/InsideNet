using Microsoft.AspNetCore.SignalR;
using OnePageNet.Data.Data.Models;

namespace OnePageNet.App.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public async Task Send(MessageDto message)
    {
        await Clients.All.ReceiveMessage(message);
    }
}