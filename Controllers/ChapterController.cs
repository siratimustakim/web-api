using Microsoft.AspNetCore.Mvc;
using QuranApp.Model;
using QuranApp.Repository;

namespace QuranApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterRepository _chapterRepository;

        public ChapterController(IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }

        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Chapter>>> List()
        {
            var list = await _chapterRepository.GetChaptersAsync();
            return Ok(list);
        }

        [HttpGet("Item/{id}")]
        public async Task<ActionResult<Chapter>> Item(int id)
        {
            var chapter = await _chapterRepository.GetChapterByIdAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }
            return Ok(chapter);
        }
    }
}