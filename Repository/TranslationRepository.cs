using QuranApp.Model;

namespace QuranApp.Repository
{
    public interface ITranslationRepository
    {
        Task<IEnumerable<Translation>> GetAsync(int chapter_id, int verse_id);
    }

    public class TranslationRepository : ITranslationRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public TranslationRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<IEnumerable<Translation>> GetAsync(int chapter_id, int verse_id)
        {
            string sql = @"SELECT [t].[id] as Id
                                 ,[t].[chapter_id] as ChapterId
                                 ,[t].[verse_id] as VerseId
                                 ,[t].[resource_id] as ResourceId
                                 ,[t].[text] as Text
	                             ,[r].[name] as ResourceName
	                             ,[r].[publisher] as Publisher
	                             ,[a].[name] as AuthorName
                             FROM [dbo].[Translation] t
                             LEFT JOIN [dbo].[Resource] r
                             ON [t].[resource_id] = [r].[id]
                             LEFT JOIN [dbo].[Author] a
                             ON [r].[author_id] = [a].[id]
                             WHERE [t].[chapter_id] = @ChapterId AND [t].[verse_id] = @Id";

            return await _databaseConnection.QueryAsync<Translation>(sql, new { ChapterId = chapter_id, Id = verse_id });
        }
    }
}
