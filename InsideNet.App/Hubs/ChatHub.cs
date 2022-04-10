using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using InsideNet.Data.Data;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;

namespace InsideNet.App.Hubs;

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
        try
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
            await _dbContext.MessageEntities.AddAsync(_mapper.Map<MessageEntity>(message));
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}