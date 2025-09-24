using Microsoft.EntityFrameworkCore;
using GigTracker.Api.Models;

namespace GigTracker.Api.Data
{
    public class GigTrackerDbContext(DbContextOptions<GigTrackerDbContext> options) : DbContext(options)
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<BandMember> BandMembers { get; set; }
        public DbSet<Gig> Gigs { get; set; }
    }
}
