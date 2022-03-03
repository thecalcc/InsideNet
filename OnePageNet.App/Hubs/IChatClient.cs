﻿using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(MessageDto messageDto);
}