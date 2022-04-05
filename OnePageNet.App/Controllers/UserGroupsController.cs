using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupsController : Controller
    {
        IUserGroupService _userGroupService;
        public UserGroupsController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [HttpGet]
        [Route("get-all/{userId}")]
        public async Task<ActionResult<UserGroupDTO>> GetAll([FromRoute] string userId)
        {
            var dtos = await _userGroupService.GetAll(userId);

            if (!dtos.Any() || dtos?.Count == null) return NotFound("There are no such entities in the database.");

            return Ok(dtos);
        }

        [Route("get/{id}")]
        [HttpGet]
        public async Task<ActionResult<UserGroupDTO>> Get(string id)
        {
            var dto = await _userGroupService.GetById(id);
            if (string.IsNullOrEmpty(dto.Id)) return NotFound();

            return Ok(dto);
        }


        [Route("create")]
        [HttpPost]
        public virtual async Task<ActionResult<UserGroupDTO>> Create([FromBody] UserGroupDTO dto)
        {
            await _userGroupService.AddAsync(dto.GroupId, dto.UsersId);
            string id = await _userGroupService.GetIdByComposite(dto.UsersId, dto.GroupId);
            return CreatedAtAction("Get", id);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var entity = await _userGroupService.GetById(id);
            if (string.IsNullOrEmpty(entity.Id)) return NotFound();

            var deleted = await _userGroupService.DeleteAsync(entity);

            if (deleted) return Ok(deleted);
            return BadRequest("Didn't delete the entity successfully.");
        }

    }
}
