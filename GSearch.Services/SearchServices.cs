
using Microsoft.Extensions.Configuration;
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
        public const string divSearchResultRegexPatternDefault = @"<div class=""ZINbbc xpd O9g5cc uUPGi"">";
        public string DivSearchResultRegexPattern { get; private set; }

        private GoogleSearch _googleSearch;
        private ILogger<SearchServices> _logger;
        private IConfiguration _configuration;
        public SearchServices(GoogleSearch googlesearch, ILogger<SearchServices> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _googleSearch = googlesearch;
            _logger = logger;
            DivSearchResultRegexPattern = _configuration.GetSection("SearchResultHtmlDivRegex").GetValue<string>("google") ?? divSearchResultRegexPatternDefault;

        }
        public string Search(string url, string keywords, int numberofResult = 0)
        {
            var result = string.Empty;
            try
            {
                var searchresult = _googleSearch.Search(keywords, numberofResult).Result;

                var positions = searchresult.SplitIndexOf(DivSearchResultRegexPattern, url);

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
                var searchresult = await _googleSearch.Search(keywords, numberofResult);

                var positions = searchresult.SplitIndexOf(DivSearchResultRegexPattern, url);

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
