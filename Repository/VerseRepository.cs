using QuranApp.Model;

namespace QuranApp.Repository
{
    public interface IVerseRepository
    {
        Task<IEnumerable<Verse>> GeVersesByChapterId(int chapterId);
        Task<IEnumerable<Verse>> GeVersesByJuzId(int juzNumber);
        Task<IEnumerable<Verse>> GetVersesByPage(int pageNumber);
    }

    public class VerseRepository : IVerseRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public VerseRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<IEnumerable<Verse>> GeVersesByChapterId(int chapterId)
        {
            string sql = @"SELECT * FROM [dbo].[Verse] 
                           WHERE chapter_id = @ChapterId";
            return await _databaseConnection.QueryAsync<Verse>(sql, new { ChapterId = chapterId });
        }

        public async Task<IEnumerable<Verse>> GeVersesByJuzId(int juzNumber)
        {
            string sql = @"SELECT * FROM [dbo].[Verse] 
                           WHERE juz_number = @JuzNumber";
            return await _databaseConnection.QueryAsync<Verse>(sql, new { JuzNumber = juzNumber });
        }

        public async Task<IEnumerable<Verse>> GetVersesByPage(int pageNumber)
        {
            string sql = @"SELECT * FROM [dbo].[Verse] 
                           WHERE page_number = @PageNumber";
            return await _databaseConnection.QueryAsync<Verse>(sql, new { PageNumber = pageNumber });
        }
    }
}
