
using Microsoft.Extensions.Logging;
using System.Linq;
namespace GSearch.Services
{
    public interface ISearchServices {
        string Search(string url, string keywords);
    }
    public class SearchServices : ISearchServices
    {
        public const string divSearchResultRegexPattern = @"<div class=""ZINbbc xpd O9g5cc uUPGi"">";
        private GoogleSearch _googleSearch;
        private ILogger<SearchServices> _logger;
        public SearchServices(GoogleSearch googlesearch , ILogger<SearchServices> logger) {

            _googleSearch = googlesearch;
            _logger = logger;
        }
        public string Search(string url, string keywords)
        {
            var result = string.Empty;
            try
            {
                _googleSearch.SearchQuery = keywords;
                var searchresult = _googleSearch.Search().Result;

                var positions = searchresult.SplitIndexOf(divSearchResultRegexPattern, url);

                if (positions.Any())
                {
                    result = string.Join(",", positions.Select(i => i.ToString()));
                    
                }
                else
                    result = "0";
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            return result;
        }
    }
}
