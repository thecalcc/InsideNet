#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupEntitiesController : ControllerBase
    {
        private readonly OnePageNetDbContext _context;

        public GroupEntitiesController(OnePageNetDbContext context)
        {
            _context = context;
        }

        // GET: api/GroupEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupEntity>>> GetGroupEntities()
        {
            return await _context.GroupEntities.ToListAsync();
        }

        // GET: api/GroupEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupEntity>> GetGroupEntity(int id)
        {
            var groupEntity = await _context.GroupEntities.FindAsync(id);

            if (groupEntity == null)
            {
                return NotFound();
            }

            return groupEntity;
        }

        // PUT: api/GroupEntities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupEntity(int id, GroupEntity groupEntity)
        {
            if (id != groupEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(groupEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupEntityExists(id))
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

        // POST: api/GroupEntities
        [HttpPost]
        public async Task<ActionResult<GroupEntity>> PostGroupEntity(GroupEntity groupEntity)
        {
            _context.GroupEntities.Add(groupEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupEntity", new { id = groupEntity.Id }, groupEntity);
        }

        // DELETE: api/GroupEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupEntity(int id)
        {
            var groupEntity = await _context.GroupEntities.FindAsync(id);
            if (groupEntity == null)
            {
                return NotFound();
            }

            _context.GroupEntities.Remove(groupEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupEntityExists(int id)
        {
            return _context.GroupEntities.Any(e => e.Id == id);
        }
    }
}
