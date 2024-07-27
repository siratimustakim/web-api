namespace QuranApp.Model
{
    public class Resource
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public required string Name { get; set; }
        public required string Publisher { get; set; }
    }
}