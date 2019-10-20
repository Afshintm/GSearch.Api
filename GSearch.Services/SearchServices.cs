
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace GSearch.Services
{
    public interface ISearchServices {
        string Search(string url, string keywords, int numberofResult = 0);
        Task<string> Search_v1(string url, string keywords, int numberofResult = 0);
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
        public string Search(string url, string keywords,int numberofResult=0)
        {
            var result = string.Empty;
            try
            {
                _googleSearch.SearchQuery = keywords;
                _googleSearch.NumberOfResult = numberofResult;
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
        public async Task<string> Search_v1(string url, string keywords, int numberofResult = 0)
        {
            var result = string.Empty;
            try
            {
                _googleSearch.SearchQuery = keywords;
                _googleSearch.NumberOfResult = numberofResult;
                var searchresult = await _googleSearch.Search();

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
