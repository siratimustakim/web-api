namespace QuranApp.Model
{
    public class Chapter
    {
        public int Id { get; set; }
        public required string RevelationPlace { get; set; }
        public bool BismillahPre { get; set; }
        public required string NameSimple { get; set; }
        public required string NameArabic { get; set; }
        public int VersesCount { get; set; }
        public required string AudioUrl { get; set; }
    }
}