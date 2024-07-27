namespace QuranApp.Model
{
    public class Translation
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public int VerseId { get; set; }
        public int ResourceId { get; set; }
        public required string Text { get; set; }

        public required string AuthorName { get; set; }
        public required string ResourceName { get; set; }
        public required string Publisher { get; set; }
    }
}