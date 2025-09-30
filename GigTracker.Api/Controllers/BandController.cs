using Microsoft.AspNetCore.Mvc;
using GigTracker.Api.Data;
using GigTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GigTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BandController(GigTrackerDbContext context) : ControllerBase
    {
        private readonly GigTrackerDbContext _context = context;

        // GET: /Band
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Band>>> GetBands()
        {
            return await _context.Bands
                .Include(b => b.Members)
                .ToListAsync();
        }

        // GET: /Band/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Band>> GetBand(int id)
        {
            var band = await _context.Bands
                .Include(b => b.Members)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (band == null)
                return NotFound();

            return band;
        }

        // POST: /Band
        [HttpPost]
        public async Task<ActionResult<Band>> CreateBand(Band band)
        {
            // If band.Members contains only Ids, fetch the full entities from the database
            if (band.Members != null && band.Members.Count > 0)
            {
                var memberIds = band.Members.Select(m => m.Id).ToList();
                var existingMembers = await _context.BandMembers
                    .Where(m => memberIds.Contains(m.Id))
                    .ToListAsync();

                band.Members = existingMembers;
            }

            _context.Bands.Add(band);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBand), new { id = band.Id }, band);
        }

        // PUT: /Band/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBand(int id, Band band)
        {
            if (id != band.Id)
                return BadRequest();

            _context.Entry(band).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Bands.Any(b => b.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: /Band/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBand(int id)
        {
            var band = await _context.Bands.FindAsync(id);
            if (band == null)
                return NotFound();

            _context.Bands.Remove(band);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}