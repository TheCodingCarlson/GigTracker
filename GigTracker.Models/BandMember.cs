namespace GigTracker.Models
{
    public class BandMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? Age { get; set; }
        public ICollection<string> Instruments { get; set; } = [];

        // Navigation property: Many-to-many with Band
        public ICollection<Band> Bands { get; set; } = [];
    }
}
