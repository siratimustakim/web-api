namespace QuranApp.Model
{
    public class Chapter
    {
        public int id { get; set; }
        public string revelation_place { get; set; }
        public bool bismillah_pre { get; set; }
        public string name_simple { get; set; }
        public string name_arabic { get; set; }
        public int verses_count { get; set; }
        public string audio_url{ get; set; }
    }
}