using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;

namespace OnePageNet.App.Hubs;

public class ChatHub : Hub
{
    private readonly OnePageNetDbContext _dbContext;
    private readonly IMapper _mapper;

    public ChatHub(OnePageNetDbContext dbContext, IMapper mapper)
    {
        this._dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task SendMessage(MessageDto message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);

        await _dbContext.MessageEntities.AddAsync(_mapper.Map<MessageEntity>(message));

        await _dbContext.SaveChangesAsync();
    }
}