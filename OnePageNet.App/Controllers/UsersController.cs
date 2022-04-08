using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("get-all-friends/{userId}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllFriends([FromRoute] string userId)
    {
        var dtos = await _userService.GetAllFriends(userId);

        if (!dtos.Any() || dtos?.Count == null) return NotFound("You don't have any friends.");

        return Ok(dtos);
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var dtos = await _userService.GetAll();

        if (!dtos.Any() || dtos?.Count == null) return NotFound("There are no users in the database.");

        return Ok(dtos);
    }

    [Route("get/{id}")]
    [HttpGet]
    public async Task<ActionResult<UserDto>> Get(string id)
    {
        var entity = await _userService.GetById(id);
        if (string.IsNullOrEmpty(entity.Id)) return NotFound("The is no such NPC in the database.");

        return Ok(entity);
    }

    [Route("update/{id}")]
    [HttpPut]
    public async Task<IActionResult> UpdateEntity([FromRoute] string id, [FromBody] UserDto dto)
    {
        if (id != dto.Id) return BadRequest();

        _userService.Update(dto);

        try
        {
            await _userService.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_userService.Exists(id)) return NotFound();
            throw new Exception("NPC doesn't exist");
        }

        return NoContent();
    }

    [Route("delete/{id}")]
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        if (!await _userService.RemoveAsync(id)) return NotFound();

        await _userService.SaveChangesAsync();
        return Ok();
    }

    [Route("get-filtered-users/{search}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetFilteredUsers(string search)
    {
        var dtos = await _userService.GetFilteredUsers(search);

        if (!dtos.Any() || dtos?.Count == null) return NotFound("There are no users who match the search");

        return Ok(dtos);
    }
}