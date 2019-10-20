using GSearch.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get([FromQuery]string keywords,[FromQuery]string url)
        {
            if (string.IsNullOrEmpty(keywords) || string.IsNullOrEmpty(url))
                return BadRequest("Request should be in /search?keywords=Some keywords&url=Target url");
            //var result = _searchServices.Search("www.infotrack.com", "title search australia");
            var result = _searchServices.Search(url, keywords);
            return Ok(result);
        }

    }
}
