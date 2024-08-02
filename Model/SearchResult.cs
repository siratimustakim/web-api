namespace QuranApp.Model
{
    public class SearchResult
    {
        public required IEnumerable<Chapter> Chapters { get; set; }
        public required IEnumerable<Translation> Verses { get; set; }

        public int TotalChapters => Chapters?.Count() ?? 0;
        public int TotalVerses => Verses?.Count() ?? 0;
    }
}