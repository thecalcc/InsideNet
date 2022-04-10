using Microsoft.AspNetCore.Mvc;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;
using InsideNet.Services.Services.Interfaces;

namespace InsideNet.App.Controllers
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
        public async Task<ActionResult<IEnumerable<UserRelationDto>>> GetAll([FromRoute] string userId)
        {
            var dtos = await _userRelationsService.GetAll(userId);

            if (!dtos.Any() || dtos?.Count == 0) return NotFound("There are no userRelationEntities in the database.");

            return Ok(dtos);
        }

        [HttpGet("get-by-id/{currentUserId}/{targetUserId}")]
        public async Task<ActionResult<IEnumerable<UserRelationDto>>> GetById(string currentUserId,
            string targetUserId)
        {
            var dto = await _userRelationsService.GetById(currentUserId, targetUserId);

            if (dto.Id == null) return NotFound("There are no such entities in the database.");

            return Ok(dto);
        }

        [HttpPost("create")]
        public async Task<ActionResult<IEnumerable<UserRelationEntity>>> Create([FromBody] CreateUserRelationsDto dto)
        {
            try
            {
                await _userRelationsService.AddAsync(dto.CurrentUserId, dto.TargetUserId);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok();
        }

        [HttpPost("update")]
        public async Task<ActionResult<bool>> Update([FromBody] UpdateUserRelationsDto dto)
        {
            var updated = await _userRelationsService.Update(dto);

            if (updated) return Ok(updated);
            return BadRequest(updated);
        }
    }
}