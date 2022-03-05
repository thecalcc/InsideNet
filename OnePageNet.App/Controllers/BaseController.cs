using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController<T, TG> : ControllerBase
    where T : BaseEntity
    where TG : BaseDTO
{
    private readonly IDatabaseService<TG, T> _databaseService;
    private readonly IMapper _mapper;

    protected BaseController(
        IDatabaseService<TG, T> databaseService,
        IMapper mapper)
    {
        _databaseService = databaseService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<ActionResult<IEnumerable<TG>>> GetAll()
    {
        var dtos = await _databaseService.ToListAsync();
        
        if (!dtos.Any() || dtos?.Count == null) return NotFound("There are no such entities in the database.");

        return Ok(dtos);
    }

    [Route("get/{id}")]
    [HttpGet]
    public async Task<ActionResult<TG>> Get(string id)
    {
        var entity = await _databaseService.FindById(id);
        if (string.IsNullOrEmpty(entity.Id)) return NotFound();

        return Ok(entity);
    }

    [Route("update/{id}")]
    [HttpPut]
    public async Task<IActionResult> UpdateEntity([FromRoute] string id,[FromBody] TG dto)
    {
        if (id != dto.Id) return BadRequest();
        
        _databaseService.Update(dto);

        try
        {
            await _databaseService.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_databaseService.Exists(id)) return NotFound();

            throw;
        }

        return NoContent();
    }

    [Route("create")]
    [HttpPost]
    public async Task<ActionResult<TG>> Create([FromBody] TG dto)
    {
        await _databaseService.AttachUser(dto);
        await _databaseService.AddAsync(dto);
        return CreatedAtAction("Get", new {id = dto.Id}, dto);
    }

    [Route("delete/{id}")]
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var entity = await _databaseService.FindById(id);
        if (string.IsNullOrEmpty(entity.Id)) return NotFound();

        _databaseService.Remove(entity);
        await _databaseService.SaveChangesAsync();

        return NoContent();
    }
}