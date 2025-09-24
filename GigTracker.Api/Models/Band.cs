namespace GigTracker.Api.Models
{
    public class Band
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<string> Genres { get; set; } = [];

        // Navigation property: Many-to-many with BandMember
        public ICollection<BandMember> Members { get; set; } = [];
    }
}