using Microsoft.AspNetCore.Mvc;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;
using InsideNet.Services.Services.Interfaces;

namespace InsideNet.App.Controllers;

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
        try
        {
            return Ok(await _databaseService.GetAllById(group));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}