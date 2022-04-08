﻿using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : BaseController<MessageEntity, MessageDto>
{
    private readonly IMessageEntityDatabaseService _databaseService;

    public MessagesController(IMessageEntityDatabaseService databaseService)
        : base(databaseService)
    {
        _databaseService = databaseService;
    }

    [Route("get-history/{group}")]
    [HttpGet]
    public async Task<ActionResult<List<MessageDto>>> GetGroupHistory([FromRoute] string group)
    {
        var dto = await _databaseService.GetAllById(group);

        if (dto.First().Id == null)
        {
            return BadRequest(group);
        }

        return Ok(dto);
    }
}