namespace GigTracker.Api.Models
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation property: Many-to-many with BandMember
        public ICollection<BandMember> Players { get; set; } = [];
    }
}