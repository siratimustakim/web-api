using Microsoft.AspNetCore.Mvc;
using QuranApp.Model;
using QuranApp.Repository;

namespace QuranApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VerseController : ControllerBase
    {
        private readonly IVerseRepository _verseRepository;

        public VerseController(IVerseRepository verseRepository)
        {
            _verseRepository = verseRepository;
        }

        [HttpGet("ChapterId/{chapter_id}")]
        public async Task<ActionResult<VerseList>> GetByChapterId(int chapter_id, [FromQuery] int page_number)
        {
            VerseList verses = new VerseList();

            if (page_number > 0) verses = await _verseRepository.GetVersesByChapterId(chapter_id, page_number);
            if (verses == null) return NotFound();

            return Ok(verses);
        }

        [HttpGet("GetVerseById")]
        public async Task<ActionResult<Verse>> GetVerseById([FromQuery] int chapter_id, [FromQuery] int verse_id)
        {
            var verse = await _verseRepository.GetVerseById(chapter_id, verse_id);
            if (verse == null) return NotFound();

            return Ok(verse);
        }

        [HttpGet("JuzId/{juz_id}")]
        public async Task<ActionResult<IEnumerable<Verse>>> GetByJuzId(int juz_id)
        {
            var verses = await _verseRepository.GeVersesByJuzId(juz_id);
            if (verses == null)
            {
                return NotFound();
            }
            return Ok(verses);
        }

        [HttpGet("PageId/{page_id}")]
        public async Task<ActionResult<IEnumerable<Verse>>> GetByPage(int page_id)
        {
            var verses = await _verseRepository.GetVersesByPage(page_id);
            if (verses == null)
            {
                return NotFound();
            }
            return Ok(verses);
        }
    }
}