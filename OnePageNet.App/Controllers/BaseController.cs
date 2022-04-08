using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController<T, TG> : ControllerBase
    where T : BaseEntity
    where TG : BaseDTO
{
    private readonly IDatabaseService<T, TG> _databaseService;

    protected BaseController(
        IDatabaseService<T, TG> databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<ActionResult<IEnumerable<TG>>> GetAll()
    {
        try
        {
            var dtos = await _databaseService.ToListAsync();
            if (!dtos.Any() || dtos?.Count == null) return NotFound("There are no such entities in the database.");
            return Ok(dtos);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Route("get/{id}")]
    [HttpGet]
    public async Task<ActionResult<TG>> Get(string id)
    {
        try
        {
            var dto = await _databaseService.FindById(id);
            if (string.IsNullOrEmpty(dto.Id)) return NotFound();

            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Route("update/{id}")]
    [HttpPut]
    public async Task<IActionResult> UpdateEntity([FromRoute] string id, [FromBody] TG dto)
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

        return Ok(dto);
    }

    [Route("create")]
    [HttpPost]
    public virtual async Task<ActionResult<TG>> Create([FromBody] TG dto)
    {
        try
        {
            await _databaseService.AttachUser(dto);
            await _databaseService.AddAsync(dto);
            return CreatedAtAction("Get", dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Route("delete/{id}")]
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var deleted = await _databaseService.Remove(id);
            if (deleted) return Ok(deleted);

            return BadRequest("Didn't delete the entity successfully.");
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}