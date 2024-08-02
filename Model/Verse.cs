namespace QuranApp.Model
{
    public class VerseList
    {
        public List<Verse> Verses { get; set; }
        public Pagination Pagination { get; set; }
    }

    public class Verse
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public int PageNumber { get; set; }
        public int VerseNumber { get; set; }
        public string ArabicVerseNumber { get; set; }
        public required string VerseKey { get; set; }
        public int JuzNumber { get; set; }
        public required string Text { get; set; }
        public IEnumerable<Translation> Translations { get; set; }
    }
}