using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly Mapper _mapper;

    public UsersController(IUserService userService, Mapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var entities = await _userService.GetAll();

        if (!entities.Any() || entities?.Count == null) return NotFound("There are no users in the database.");

        var dtos = _mapper.Map<List<UserDto>>(entities);

        return Ok(dtos);
    }

    [Route("get/{id}")]
    [HttpGet]
    public async Task<ActionResult<UserDto>> Get(string id)
    {
        var entity = await _userService.GetById(id);
        if (string.IsNullOrEmpty(entity.Id)) return NotFound();

        return Ok(_mapper.Map<UserDto>(entity));
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

    [Route("create")]
    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] UserDto userDto)
    {
        await _userService.AttachUser(userDto);
        await _userService.AddAsync(userDto);
        return CreatedAtAction("Get", new {id = userDto.Id}, userDto);
    }

    [Route("delete/{id}")]
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        if (!await _userService.Remove(id)) return NotFound();

        await _userService.SaveChangesAsync();
        return Ok();
    }
}