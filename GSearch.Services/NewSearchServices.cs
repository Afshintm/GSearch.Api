using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace GSearch.Services
{
   
    public class NewSearchServices : ISearchServices
    {
        public const string divSearchResultRegexPattern = @"<div class=""ZINbbc xpd O9g5cc uUPGi"">";
        private NewGoogleSearch _googleSearch;
        private ILogger<NewSearchServices> _logger;
        public NewSearchServices(NewGoogleSearch googlesearch, ILogger<NewSearchServices> logger)
        {

            _googleSearch = googlesearch;
            _logger = logger;
        }
        //private void SetParams(string keywords, int numberofResult)
        //{
        //    _googleSearch.SearchQuery = keywords;
        //    _googleSearch.NumberOfResult = numberofResult;
        //}
        public string Search(string url, string keywords, int numberofResult = 0)
        {
            var result = string.Empty;
            try
            {
                //SetParams(keywords, numberofResult);
                var searchresult = _googleSearch.Search(keywords, numberofResult).Result;

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
                //SetParams(keywords, numberofResult);
                var searchresult = await _googleSearch.Search(keywords, numberofResult);

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
