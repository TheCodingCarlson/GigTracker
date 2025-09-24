using Microsoft.AspNetCore.Mvc;
using GigTracker.Api.Data;
using GigTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GigTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GigController : ControllerBase
    {
        private readonly GigTrackerDbContext _context;

        public GigController(GigTrackerDbContext context)
        {
            _context = context;
        }

        // GET: /Gig
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gig>>> GetGigs()
        {
            return await _context.Gigs
                .Include(g => g.Band)
                    .ThenInclude(b => b.Members)
                .ToListAsync();
        }

        // GET: /Gig/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Gig>> GetGig(int id)
        {
            var gig = await _context.Gigs
                .Include(g => g.Band)
                    .ThenInclude(b => b.Members)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gig == null)
                return NotFound();

            return gig;
        }

        // POST: /Gig
        [HttpPost]
        public async Task<ActionResult<Gig>> CreateGig(Gig gig)
        {
            // If gig.Band is provided with an Id, fetch the existing band
            if (gig.Band != null && gig.Band.Id > 0)
            {
                var existingBand = await _context.Bands.FindAsync(gig.Band.Id);
                if (existingBand == null)
                    return BadRequest("Band not found.");

                gig.Band = existingBand;
                gig.BandId = existingBand.Id;
            }

            _context.Gigs.Add(gig);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGig), new { id = gig.Id }, gig);
        }

        // PUT: /Gig/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGig(int id, Gig gig)
        {
            if (id != gig.Id)
                return BadRequest();

            _context.Entry(gig).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Gigs.Any(g => g.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: /Gig/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGig(int id)
        {
            var gig = await _context.Gigs.FindAsync(id);
            if (gig == null)
                return NotFound();

            _context.Gigs.Remove(gig);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}