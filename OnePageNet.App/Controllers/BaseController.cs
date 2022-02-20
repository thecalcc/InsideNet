using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController<T, TG> : ControllerBase
    where T : BaseEntity
    where TG : BaseDTO
{
    private readonly IDatabaseService<T> _databaseService;
    private readonly IMapper _mapper;

    protected BaseController(
        IDatabaseService<T> databaseService,
        IMapper mapper)
    {
        _databaseService = databaseService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<ActionResult<IEnumerable<TG>>> GetAll()
    {
        var entities = await _databaseService.ToListAsync();

        var dtos = (List<TG>) _mapper.Map<IEnumerable<TG>>(entities);

        if (!dtos.Any() || dtos?.Count == null) return NotFound("There are no such entities in the database.");

        return Ok(dtos);
    }

    [Route("get/{Id}")]
    [HttpGet]
    public async Task<ActionResult<TG>> Get(string Id)
    {
        var entity = await _databaseService.FindById(Id);
        if (string.IsNullOrEmpty(entity.Id)) return NotFound();

        return Ok(_mapper.Map<TG>(entity));
    }

    [Route("update/{Id}")]
    [HttpPut]
    public async Task<IActionResult> UpdateEntity([FromRoute] string Id,[FromBody] TG dto)
    {
        if (Id != dto.Id) return BadRequest();

        var entity = _mapper.Map<T>(dto);

        _databaseService.Update(entity);

        try
        {
            await _databaseService.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_databaseService.Exists(Id)) return NotFound();

            throw;
        }

        return NoContent();
    }

    [Route("create")]
    [HttpPost]
    public async Task<ActionResult<TG>> Create([FromBody] TG dto)
    {
        var entity = _mapper.Map<T>(dto);
        await _databaseService.AttachUser(entity);
        await _databaseService.AddAsync(entity);
        return CreatedAtAction("Get", new {id = dto.Id}, dto);
    }

    [Route("delete/{Id}")]
    [HttpDelete]
    public async Task<IActionResult> Delete(string Id)
    {
        var entity = await _databaseService.FindById(Id);
        if (string.IsNullOrEmpty(entity.Id)) return NotFound();

        _databaseService.Remove(entity);
        await _databaseService.SaveChangesAsync();

        return NoContent();
    }
}