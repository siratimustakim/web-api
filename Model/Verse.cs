namespace QuranApp.Model
{
    public class Verse
    {
        public int id { get; set; }
        public int chapter_id { get; set; }
        public int page_number { get; set; }
        public int verse_number { get; set; }
        public string verse_key { get; set; }
        public int juz_number { get; set; }
        public string text { get; set; }
    }
}