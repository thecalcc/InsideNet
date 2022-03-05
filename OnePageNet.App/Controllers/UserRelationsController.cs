using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

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
        public async Task<ActionResult<IEnumerable<UserRelationEntity>>> GetAll([FromRoute] string userId)
        {
            var dtos = await _userRelationsService.GetAll(userId);

            if (!dtos.Any() || dtos?.Count == null) return NotFound("There are no such entities in the database.");

            return Ok(dtos);
        }

        [HttpGet("get-by-id/{currentUserId}/{targetUserId}")]
        public async Task<ActionResult<IEnumerable<UserRelationEntity>>> GetById(string currentUserId,
            string targetUserId)
        {
            var dto = await _userRelationsService.GetById(currentUserId, targetUserId);

            if (dto.Id == null) return NotFound("There are no such entities in the database.");

            return Ok(dto);
        }

        [HttpPost("create")]
        public async Task<ActionResult<IEnumerable<UserRelationEntity>>> Create([FromBody] UserRelationsDto dto)
        {
            await _userRelationsService.AddAsync(dto.CurrentUser, dto.TargetUser);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<ActionResult<bool>> Update(string currentUserId, string targetUserId, string command)
        {
            var updated = await _userRelationsService.Update(currentUserId, targetUserId, command);
            
            if (updated) return Ok(updated);
            return BadRequest(updated);
        }
    }
}