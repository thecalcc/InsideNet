using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRelationsController:Controller
    {
        private readonly IUserRelationsService _userRelationsService;
        private readonly IMapper _mapper;

        public UserRelationsController(IUserRelationsService userRelationsService, IMapper mapper)
        {
            _userRelationsService = userRelationsService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-all/{userId}")]
        public async Task<ActionResult<IEnumerable<UserRelationEntity>>> GetAll([FromRoute]string userId)
        {
            var dtos = await _userRelationsService.GetAll(userId);

            if (!dtos.Any() || dtos?.Count == null) return NotFound("There are no such entities in the database.");

            return Ok(dtos);
        }
        [HttpGet]
        [Route("get-by-id/{currentUserId}/{targetUserId}")]
        public async Task<ActionResult<IEnumerable<UserRelationEntity>>> GetById(string currentUserId, string targetUserId)
        {
            var dto = await _userRelationsService.GetById(currentUserId, targetUserId);

            if (dto == null) return NotFound("There are no such entities in the database.");

            return Ok(dto);
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<IEnumerable<UserRelationEntity>>> Create([FromBody] UserRelationsDto dto)
        {
            await _userRelationsService.AddAsync(dto.CurrentUser, dto.TargetUser, dto.UserRelationship);

            return Ok();
        }
    }
}
