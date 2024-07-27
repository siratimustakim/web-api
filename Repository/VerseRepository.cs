using QuranApp.Model;
using QuranApp.Util;

namespace QuranApp.Repository
{
    public interface IVerseRepository
    {
        Task<IEnumerable<Verse>> GetVersesByChapterId(int chapterId);
        Task<IEnumerable<Verse>> GeVersesByJuzId(int juzNumber);
        Task<IEnumerable<Verse>> GetVersesByPage(int pageNumber);
    }

    public class VerseRepository : IVerseRepository
    {
        private readonly IDatabaseConnection _databaseConnection;
        private readonly ITranslationRepository _translationRepository;

        public VerseRepository(IDatabaseConnection databaseConnection, ITranslationRepository translationRepository)
        {
            _databaseConnection = databaseConnection;
            _translationRepository = translationRepository;
        }

        public async Task<IEnumerable<Verse>> GetVersesByChapterId(int chapterId)
        {
            string sql = @"SELECT  [id] as Id
                                  ,[chapter_id] as ChapterId
                                  ,[page_number] as PageNumber
                                  ,[verse_number] as VerseNumber
                                  ,[verse_key] as VerseKey
                                  ,[juz_number] as JuzNumber
                                  ,[text] as Text
                           FROM [dbo].[Verse] 
                           WHERE chapter_id = @ChapterId
                           ORDER BY id";
            var verses = await _databaseConnection.QueryAsync<Verse>(sql, new { ChapterId = chapterId });

            var tasks = verses.Select(async verse =>
            {
                var translation = await _translationRepository.GetAsync(chapterId, verse.VerseNumber);
                return new Verse
                {
                    Id = verse.Id,
                    ChapterId = verse.ChapterId,
                    JuzNumber = verse.JuzNumber,
                    VerseKey = verse.VerseKey,
                    PageNumber = verse.PageNumber,
                    VerseNumber = verse.VerseNumber,
                    ArabicVerseNumber = Utils.GetArabicNumber(verse.VerseNumber.ToString()),
                    Text = verse.Text,
                    Translations = translation
                };
            });

            var newList = await Task.WhenAll(tasks);
            return newList.ToList();
        }

        public async Task<IEnumerable<Verse>> GeVersesByJuzId(int juzNumber)
        {
            string sql = @"SELECT  [id]
                                  ,[chapter_id]
                                  ,[page_number]
                                  ,[verse_number]
                                  ,[verse_key]
                                  ,[juz_number]
                                  ,[text]
                           FROM [dbo].[Verse] 
                           WHERE juz_number = @JuzNumber";
            return await _databaseConnection.QueryAsync<Verse>(sql, new { JuzNumber = juzNumber });
        }

        public async Task<IEnumerable<Verse>> GetVersesByPage(int pageNumber)
        {
            string sql = @"SELECT  [id] as Id
                                  ,[chapter_id] as ChapterId
                                  ,[page_number] as PageNumber
                                  ,[verse_number] as VerseNumber
                                  ,[verse_key] as VerseKey
                                  ,[juz_number] as JuzNumber
                                  ,[text] as Text
                           FROM [dbo].[Verse]
                           WHERE page_number = @PageNumber";
            var list = await _databaseConnection.QueryAsync<Verse>(sql, new { PageNumber = pageNumber });
            list.ToList().ForEach(v => { v.ArabicVerseNumber = Utils.GetArabicNumber(v.VerseNumber.ToString()); });
            return list;
        }
    }
}