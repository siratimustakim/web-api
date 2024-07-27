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
                               [c].[id] as Id
                              ,[c].[revelation_place] as RevelationPlace
                              ,[c].[bismillah_pre] as BismillahPre
                              ,[c].[name_simple] as NameSimple
                              ,[c].[name_arabic] as NameArabic
                              ,[c].[verses_count] as VersesCount
                              ,[a].[audio_url] as AudioUrl
                           FROM [dbo].[Chapter] c
                           LEFT JOIN [dbo].[Audio] a
                           ON [c].[id] = [a].[chapter_id]";
            return await _databaseConnection.QueryAsync<Chapter>(sql);
        }

        public async Task<Chapter> GetChapterByIdAsync(int id)
        {
            string sql = @"SELECT 
                               [c].[id] as Id
                              ,[c].[revelation_place] as RevelationPlace
                              ,[c].[bismillah_pre] as BismillahPre
                              ,[c].[name_simple] as NameSimple
                              ,[c].[name_arabic] as NameArabic
                              ,[c].[verses_count] as VersesCount
                              ,[a].[audio_url] as AudioUrl
                           FROM [dbo].[Chapter] c
                           LEFT JOIN [dbo].[Audio] a
                           ON [c].[id] = [a].[chapter_id]
                           WHERE [c].[id] = @Id";
            var result = await _databaseConnection.QueryAsync<Chapter>(sql, new { Id = id });
            return result.FirstOrDefault();
        }
    }
}
