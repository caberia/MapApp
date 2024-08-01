using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapApp.Data;

namespace MapApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PointController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Point>>> GetPoints()
        {
            var points = await _context.Points
                .OrderBy(p => p.Id)
                .ToListAsync();

            return Ok(points);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Point>> GetPoint(int id)
        {
            var point = await _context.Points.FindAsync(id);

            if (point == null)
            {
                return NotFound();
            }

            return point;
        }

        [HttpPost]
        public async Task<ActionResult<Point>> CreatePoint([FromBody] Point point)
        {
            if (point == null)
            {
                return BadRequest("Point data is null.");
            }

            _context.Points.Add(point);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPoint), new { id = point.Id }, point);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePoint(int id, [FromBody] Point point)
        {
            if (id != point.Id)
            {
                return BadRequest("Point ID mismatch.");
            }

            _context.Entry(point).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoint(int id)
        {
            var point = await _context.Points.FindAsync(id);
            if (point == null)
            {
                return NotFound();
            }

            _context.Points.Remove(point);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PointExists(int id)
        {
            return _context.Points.Any(e => e.Id == id);
        }
    }
}
