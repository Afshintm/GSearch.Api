using GSearch.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GSearch.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchServices _searchServices;
        private readonly IGenericSearchServices<BingSearchEngine> _bingSearchServices;
        public SearchController(ISearchServices searchServices, IGenericSearchServices<BingSearchEngine> bingSearchServices) {
            _searchServices = searchServices;
            _bingSearchServices = bingSearchServices;
        }
        
        // GET: Search
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]string keywords, [FromQuery]string url, [FromQuery]int num = 0)
        {
            if (string.IsNullOrEmpty(keywords) || string.IsNullOrEmpty(url))
                return BadRequest("Request should be in /search?keywords=Some keywords&url=Target url");
            var result = _searchServices.SearchAsync(url, keywords, num);
            return Ok(await result);
        }

        // GET: Search/v1
        [HttpGet]
        [Route("v1")]
        public IActionResult Get([FromQuery]string keywords, [FromQuery]string url, [FromQuery]int num = 0)
        {
            if (string.IsNullOrEmpty(keywords) || string.IsNullOrEmpty(url))
                return BadRequest("Request should be in /search/v1?keywords=Some keywords&url=Target url");
            var result = _searchServices.Search(url, keywords, num);
            return Ok(result);
        }
        // GET: health
        [HttpGet]
        [Route("~/health")]
        public IActionResult Get()
        {
            return Ok("Api is healthy");
        }


        // GET: Search
        [Route("~/bing/search")]
        [HttpGet]
        public async Task<IActionResult> GetBingAsync([FromQuery]string keywords, [FromQuery]string url, [FromQuery]int num = 0)
        {
            if (string.IsNullOrEmpty(keywords) || string.IsNullOrEmpty(url))
                return BadRequest("Request should be in /bing/search?keywords=Some keywords&url=Target url");
            var result = _bingSearchServices.SearchAsync(url, keywords, num);
            return Ok(await result);
        }

    }
}
