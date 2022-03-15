using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRelationsController : Controller
    {
        private readonly IUserRelationsService _userRelationsService;

        public UserRelationsController(IUserRelationsService userRelationsService)
        {
            _userRelationsService = userRelationsService;
        }

        [HttpGet("get-all/{userId}")]
        public async Task<ActionResult<IEnumerable<UserRelationsDto>>> GetAll([FromRoute] string userId)
        {
            var dtos = await _userRelationsService.GetAll(userId);

            if (!dtos.Any() || dtos?.Count == 0) return NotFound("There are no userRelationEntities in the database.");

            return Ok(dtos);
        }

        [HttpGet("get-by-id/{currentUserId}/{targetUserId}")]
        public async Task<ActionResult<IEnumerable<UserRelationsDto>>> GetById(string currentUserId,
            string targetUserId)
        {
            var dto = await _userRelationsService.GetById(currentUserId, targetUserId);

            if (dto.Id == null) return NotFound("There are no such entities in the database.");

            return Ok(dto);
        }

        [HttpPost("create")]
        public async Task<ActionResult<IEnumerable<UserRelationEntity>>> Create([FromBody] UserRelationsDto dto)
        {
            await _userRelationsService.AddAsync(dto.CurrentUser?.Id, dto.TargetUser?.Id);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<ActionResult<bool>> Update([FromBody] UserRelationsDto dto)
        {
            var updated = await _userRelationsService.Update(dto);

            if (updated) return Ok(updated);
            return BadRequest(updated);
        }
    }
}