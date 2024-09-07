using Microsoft.AspNetCore.Mvc;
using QuranApp.Model;
using QuranApp.Repository;

namespace QuranApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchRepository _searchRepository;

        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Chapter>>> List([FromQuery] string text)
        {
            if (text.Length < 2) return NotFound();

            var list = await _searchRepository.Get(text);
            return Ok(list);
        }
    }
}