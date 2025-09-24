namespace GigTracker.Api.Models
{
    public class Gig
    {
        public int Id { get; set; }
        public string Venue { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal? Pay { get; set; }

        // Each gig has a single band
        public int BandId { get; set; }
        public Band? Band { get; set; }
    }
}