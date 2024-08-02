using QuranApp.Model;

namespace QuranApp.Repository
{
    public interface ISearchRepository
    {
        Task<SearchResult> Get(string text);
    }

    public class SearchRepository : ISearchRepository
    {
        private readonly IDatabaseConnection _databaseConnection;
        private readonly IChapterRepository _chapterRepository;
        private readonly ITranslationRepository _translationRepository;

        public SearchRepository(IDatabaseConnection databaseConnection, IChapterRepository chapterRepository, ITranslationRepository translationRepository)
        {
            _databaseConnection = databaseConnection;
            _chapterRepository = chapterRepository;
            _translationRepository = translationRepository;
        }

        public Task<SearchResult> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<SearchResult> Get(string text)
        {
            var chapterList = await _chapterRepository.GetAsync(text);
            var translationList = await _translationRepository.GetAsync(text);

            return new SearchResult { Chapters = chapterList, Verses = translationList };
        }
    }
}
