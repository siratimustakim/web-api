using Newtonsoft.Json;
using QuranApp.Model;
using QuranApp.Util;

namespace QuranApp.Repository
{
    public interface IVerseRepository
    {
        Task<VerseList> GetVersesByChapterId(int chapterId);
        Task<VerseList> GetVersesByChapterId(int chapterId, int pageNumber);
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

        public async Task<VerseList> GetVersesByChapterId(int chapterId)
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
                           ORDER BY verse_number";
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
            return new VerseList { Verses = newList.ToList() };
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
                           WHERE juz_number = @JuzNumber
                           ORDER BY verse_number";
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
                                  ,[arabic_verse_number] as ArabicVerseNumber
                           FROM [dbo].[Verse]
                           WHERE page_number = @PageNumber
                           ORDER BY verse_number";
            return await _databaseConnection.QueryAsync<Verse>(sql, new { PageNumber = pageNumber });
        }

        public async Task<VerseList> GetVersesByChapterId(int chapterId, int pageNumber)
        {
            var verseCount = Utils.ChapterVerseCount[chapterId - 1];
            var totalPages = (int)Math.Ceiling((double)verseCount / 10); // Toplam sayfa sayısını hesapla
            var nextPage = (pageNumber < totalPages) ? pageNumber + 1 : (int?)null; // Eğer mevcut sayfa son sayfa değilse, bir sonraki sayfayı hesapla, aksi takdirde null

            var pagination = new Pagination
            {
                PerPage = 10,
                CurrentPage = pageNumber,
                NextPage = nextPage,
                TotalPages = totalPages,
                TotalRecords = verseCount
            };

            string sql = @"SELECT  [id] as Id
                                   ,[chapter_id] as ChapterId
                                   ,[page_number] as PageNumber
                                   ,[verse_number] as VerseNumber
                                   ,[verse_key] as VerseKey
                                   ,[juz_number] as JuzNumber
                                   ,[text] as Text
                                   ,[arabic_verse_number] as ArabicVerseNumber
                           FROM [dbo].[Verse] 
                           WHERE chapter_id = @ChapterId
                           ORDER BY id
                           OFFSET (@PageNumber - 1) * @PageSize ROWS
                           FETCH NEXT @PageSize ROWS ONLY;";
            var verses = await _databaseConnection.QueryAsync<Verse>(sql, new { ChapterId = chapterId, PageNumber = pageNumber, PageSize = 10 });

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
                    ArabicVerseNumber = verse.ArabicVerseNumber,
                    Text = verse.Text,
                    Translations = translation
                };
            });

            var newList = await Task.WhenAll(tasks);
            return new VerseList { Verses = newList.ToList(), Pagination = pagination };
        }
    }
}