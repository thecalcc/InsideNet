using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentEntitiesController : ControllerBase
    {
        private readonly OnePageNetDbContext _context;

        public CommentEntitiesController(OnePageNetDbContext context)
        {
            _context = context;
        }

        // GET: api/CommentEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentEntities()
        {
            // TODO Implement automapper or a custom entity to DTO converter
            //return await _context.CommentEntities.ToListAsync();
            return null;
        }

        // GET: api/CommentEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetCommentEntity(string publicId)
        {
            // TODO Implement publicId to Id converter
            // then convert the entity we got to a dto

            var commentEntity = await _context.CommentEntities.FindAsync();

            if (commentEntity == null)
            {
                return NotFound();
            }

            //return commentEntity;
            return null;
        }

        // PUT: api/CommentEntities/GUID
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommentEntity(string publicId, CommentDTO commentDto)
        {
            if (publicId != commentDto.PublicId)
            {
                return BadRequest();
            }

            // TODO Convert DTO to Entity

            //_context.Entry(commentEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentEntityExists(publicId))
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

        // POST: api/CommentEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CommentEntity>> PostCommentEntity(CommentDTO commentDto)
        {
            // TODO Code cleanup - implement automapper
            var commentEntity = new CommentEntity
            {
                PublicId = commentDto.PublicId,
                ApplicationUser = commentDto.ApplicationUser,
                Content = commentDto.Content,
                CreatedAt = commentDto.CreatedAt,
                DeletedAt = commentDto.DeletedAt,
                MediaURI = commentDto.MediaURI,
                Post = commentDto.Post
            };

            _context.CommentEntities.Add(commentEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentEntity", new { id = commentEntity.Id }, commentEntity);
        }

        // DELETE: api/CommentEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentEntity(string publicId)
        {
            //var commentEntity = await _context.CommentEntities.FindAsync(id);
            //if (commentEntity == null)
            //{
            //    return NotFound();
            //}

            //_context.CommentEntities.Remove(commentEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentEntityExists(string publicId)
        {
            return _context.CommentEntities.Any(e => e.PublicId == publicId);
        }
    }
}
