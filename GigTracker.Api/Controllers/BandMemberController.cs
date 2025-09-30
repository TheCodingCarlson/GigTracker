using Microsoft.AspNetCore.Mvc;
using GigTracker.Api.Data;
using GigTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GigTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BandMemberController : ControllerBase
    {
        private readonly GigTrackerDbContext _context;

        public BandMemberController(GigTrackerDbContext context)
        {
            _context = context;
        }

        // GET: /BandMember
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BandMember>>> GetBandMembers()
        {
            return await _context.BandMembers
                .Include(m => m.Bands)
                .ToListAsync();
        }

        // GET: /BandMember/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BandMember>> GetBandMember(int id)
        {
            var member = await _context.BandMembers
                .Include(m => m.Bands)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
                return NotFound();

            return member;
        }

        // POST: /BandMember
        [HttpPost]
        public async Task<ActionResult<BandMember>> CreateBandMember(BandMember member)
        {
            _context.BandMembers.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBandMember), new { id = member.Id }, member);
        }

        // PUT: /BandMember/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBandMember(int id, BandMember member)
        {
            if (id != member.Id)
                return BadRequest();

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BandMembers.Any(m => m.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: /BandMember/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBandMember(int id)
        {
            var member = await _context.BandMembers.FindAsync(id);
            if (member == null)
                return NotFound();

            _context.BandMembers.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}