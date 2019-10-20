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
        public SearchController(ISearchServices searchServices) {
            _searchServices = searchServices;
        }
        // GET: api/Search
        [HttpGet]
        public IActionResult Get([FromQuery]string keywords,[FromQuery]string url,[FromQuery]int num=0)
        {
            if (string.IsNullOrEmpty(keywords) || string.IsNullOrEmpty(url))
                return BadRequest("Request should be in /search?keywords=Some keywords&url=Target url");
            var result = _searchServices.Search(url, keywords,num);
            return Ok(result);
        }

        [HttpGet]
        [Route("v1")]
        public async Task<IActionResult> GetAsync([FromQuery]string keywords, [FromQuery]string url, [FromQuery]int num = 0)
        {
            if (string.IsNullOrEmpty(keywords) || string.IsNullOrEmpty(url))
                return BadRequest("Request should be in /search?keywords=Some keywords&url=Target url");
            var result = _searchServices.Search_v1(url, keywords, num);
            return Ok(await result);
        }

    }
}
