using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

#nullable disable
namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : BaseController<GroupEntity, GroupDTO>
{
    IGroupService _databaseService;
    public GroupsController(IGroupService databaseService)
        : base(databaseService)
    {
        _databaseService = databaseService;
    }

    [Route("create/{creatorId}/{targetId}")]
    [HttpPost]
    public virtual async Task<ActionResult<GroupDTO>> Create([FromBody] GroupDTO dto, [FromRoute] string creatorId, [FromRoute] string targetId)
    {
        await _databaseService.AttachUser(dto);
        var check = await _databaseService.AddAsync(dto, creatorId, targetId);
        if(check) return CreatedAtAction("Get", dto);
        return BadRequest("This group already exists.");
    }
}