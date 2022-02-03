using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Data.Models.PostDTOs;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostEntitiesController : ControllerBase
    {
        // TODO 
        private readonly OnePageNetDbContext _context;
        private readonly IDatabaseService databaseService;
        private readonly IMapper mapper;

        public PostEntitiesController(
            OnePageNetDbContext context,
            IDatabaseService databaseService,
            IMapper mapper)
        {
            _context = context;
            this.databaseService = databaseService;
            this.mapper = mapper;
        }

        // GET: api/PostEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostEntities()
        {
            var entities = await _context.PostEntities.ToListAsync();

            var dtos = mapper.Map<IEnumerable<PostDTO>>(entities);

            if (dtos.Count() <= 0 || dtos?.Count() == null)
            {
                return NotFound();
            }

            return Ok(dtos);
        }

        // GET: api/PostEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPostEntity(string publicId)
        {
            var postEntity = await databaseService.FindByPublicId<PostEntity>(publicId);

            if (postEntity == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PostDTO>(postEntity));
        }

        // PUT: api/PostEntities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostEntity(string publicId, PostDTO postDto)
        {
            if (publicId != postDto.PublicId)
            {
                return BadRequest();
            }

            var postEntity = mapper.Map<PostEntity>(postDto);

            _context.Entry(postEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostEntityExists(publicId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PostEntities
        [HttpPost]
        public async Task<ActionResult<PostDTO>> CreatePostEntity(PostDTO postDto)
        {
            var postEntity = mapper.Map<PostEntity>(postDto);

            _context.PostEntities.Add(postEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostEntity", new { id = postDto.PublicId }, postDto);
        }

        // DELETE: api/PostEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostEntity(string publicId)
        {
            // TODO get by public ID

            //var postEntity = await _context.PostEntities.FindAsync(id);
            var postEntity = default(PostEntity);
            if (postEntity == null)
            {
                return NotFound();
            }

            _context.PostEntities.Remove(postEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostEntityExists(string publicId)
        {
            return _context.PostEntities.Any(e => e.PublicId == publicId);
        }
    }
}
