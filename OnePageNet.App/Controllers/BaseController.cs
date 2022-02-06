using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers
{
    [ApiController]
    public class BaseController<T> : ControllerBase where T : BaseEntity
    {
        private readonly IDatabaseService<T> _databaseService;
        private readonly IMapper _mapper;

        public BaseController(
            IDatabaseService<T> databaseService,
            IMapper mapper)
        {
            this._databaseService = databaseService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
           var entities = await _databaseService.ToListAsync();

            var dtos = (List<T>) _mapper.Map<IEnumerable<T>>(entities);
            
            if (!dtos.Any() || dtos?.Count == null)
            {
                return NotFound();
            }

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(string publicId)
        {
            var entity = await _databaseService.FindByPublicId(publicId);
            // TODO - Fix
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<T>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntity(string publicId, T dto)
        {
            if (publicId != dto.PublicId)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<T>(dto);

            _databaseService.Update(dto);

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

        [HttpPost]
        public async Task<ActionResult<T>> Create(T dto)
        {
            var entity = _mapper.Map<T>(dto);

            _databaseService.AddAsync(dto);
            await _databaseService.SaveChangesAsync();
            // TODO Fix - we should only return data and not view since we are using React as front-end
            return CreatedAtAction("Get", new { id = dto.PublicId }, dto);
        }

        [HttpDelete("{id}")]
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
