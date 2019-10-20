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
        public string Get()
        {
            var result = _searchServices.Search("www.infotrack.com", "title search australia");
            return result;
        }

        // GET: api/Search/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Search
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Search/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
