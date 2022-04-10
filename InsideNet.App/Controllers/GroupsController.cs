using Microsoft.AspNetCore.Mvc;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;
using InsideNet.Services.Services.Interfaces;

#nullable disable
namespace InsideNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : BaseController<GroupEntity, GroupDto>
{
    IGroupService _databaseService;
    public GroupsController(IGroupService databaseService)
        : base(databaseService)
    {
        _databaseService = databaseService;
    }

    [Route("create/{creatorId}/{targetId}")]
    [HttpPost]
    public virtual async Task<ActionResult<GroupDto>> Create([FromBody] GroupDto dto, [FromRoute] string creatorId, [FromRoute] string targetId)
    {
        await _databaseService.AttachUser(dto);
        var check = await _databaseService.AddAsync(dto, creatorId, targetId);
        if(check) return CreatedAtAction("Get", dto);
        return BadRequest("This group already exists.");
    }
}