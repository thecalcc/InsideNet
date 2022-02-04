using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T, TG> : ControllerBase 
        where T : BaseEntity
        where TG : BaseDto
    {
        private readonly IDatabaseService<T> _databaseService;
        private readonly IMapper _mapper;

        protected BaseController(
            IDatabaseService<T> databaseService,
            IMapper mapper)
        {
            this._databaseService = databaseService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult<IEnumerable<TG>>> GetAll()
        {
           var entities = await _databaseService.ToListAsync();

            var dtos = (List<TG>) _mapper.Map<IEnumerable<TG>>(entities);
            
            if (!dtos.Any() || dtos?.Count == null)
            {
                return NotFound($"There are no such entities in the database.");
            }

            return Ok(dtos);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<TG>> Get(string publicId)
        {
            var entity = await _databaseService.FindByPublicId(publicId);
            // TODO - Fix
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TG>(entity));
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEntity(string publicId, TG dto)
        {
            if (publicId != dto.PublicId)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<T>(dto);

            _databaseService.Update(entity);

            try
            {
                await _databaseService.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_databaseService.Exists(publicId))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpPost("create")]
        public async Task<ActionResult<TG>> Create([FromBody]TG dto)
        {
            var entity = _mapper.Map<T>(dto);
            
            await _databaseService.AddAsync(entity);
            
            // TODO Fix - we should only return data and not view since we are using React as front-end
            return CreatedAtAction("Get", new { id = dto.PublicId }, dto);
        }

        [HttpDelete("delete/{publicId}")]
        public async Task<IActionResult> Delete(string publicId)
        {

            var entity = await _databaseService.FindByPublicId(publicId);
            // TODO - Discover how Task work and how to deal with null results in tasks
            if (entity == null)
            {
                return NotFound();
            }

            _databaseService.Remove(entity);
            await _databaseService.SaveChangesAsync();

            return NoContent();
        }
    }
}
