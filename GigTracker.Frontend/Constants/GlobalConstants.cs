namespace GigTracker.Frontend.Constants
{
    public static class GlobalConstants
    {
        public const string CURRENCY_FORMAT = "#,##0.00";
        public const string DATE_FORMAT = "dd/MM/yyyy";

        public const string ENTITY_NAME_ITEM = "Item";
        public const string ENTITY_NAME_GIG = "Gig";
        public const string ENTITY_NAME_BAND = "Band";
        public const string ENTITY_NAME_BANDMEMBER = "Band Member";

        public static readonly string[] MUSICAL_GENRES =
        [
            "Rock",
            "Pop",
            "Jazz",
            "Blues",
            "Classical",
            "Country",
            "Hip Hop",
            "Electronic",
            "Folk",
            "Reggae",
            "Metal",
            "R&B",
            "Soul",
            "Punk",
            "Funk"
        ];

        public static readonly string[] INSTRUMENTS =
        [
            "Guitar",
            "Bass",
            "Drums",
            "Keys",
            "Vocals",
            "Saxophone",
            "Trumpet"
        ];
    }
}
