using QuranApp.Model;

namespace QuranApp.Repository
{
    public interface IChapterRepository
    {
        Task<IEnumerable<Chapter>> GetChaptersAsync();
        Task<Chapter> GetChapterByIdAsync(int id);
    }

    public class ChapterRepository : IChapterRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public ChapterRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<IEnumerable<Chapter>> GetChaptersAsync()
        {
            string sql = @"SELECT 
                               [c].[id]
                              ,[c].[revelation_place]
                              ,[c].[bismillah_pre]
                              ,[c].[name_simple]
                              ,[c].[name_arabic]
                              ,[c].[verses_count]
                              ,[a].[audio_url]
                           FROM [dbo].[Chapter] c
                           LEFT JOIN [dbo].[Audio] a
                           ON [c].[id] = [a].[chapter_id]";
            return await _databaseConnection.QueryAsync<Chapter>(sql);
        }
        
        public async Task<Chapter> GetChapterByIdAsync(int id)
        {
            string sql = @"SELECT 
                               [c].[id]
                              ,[c].[revelation_place]
                              ,[c].[bismillah_pre]
                              ,[c].[name_simple]
                              ,[c].[name_arabic]
                              ,[c].[verses_count]
                              ,[a].[audio_url]
                           FROM [dbo].[Chapter] c
                           LEFT JOIN [dbo].[Audio] a
                           ON [c].[id] = [a].[chapter_id]
                           WHERE id = @Id";
            var result = await _databaseConnection.QueryAsync<Chapter>(sql, new { Id = id });
            return result.FirstOrDefault();
        }
    }
}
