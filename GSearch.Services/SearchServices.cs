
using System.Linq;
namespace GSearch.Services
{
    public interface ISearchServices {
        string Search(string url, string keywords);
    }
    public class SearchServices : ISearchServices
    {
        public const string divSearchResultRegexPattern = @"<div class=""ZINbbc xpd O9g5cc uUPGi"">";
        GoogleSearch _googleSearch;
        public SearchServices(GoogleSearch googlesearch) {

            _googleSearch = googlesearch;
        }
        public string Search(string url, string keywords)
        {
            var result = string.Empty;
            _googleSearch.SearchQuery = keywords;
            var searchresult = _googleSearch.Search().Result;

            var positions = searchresult.SplitIndexOf(divSearchResultRegexPattern,url);

            if (positions.Any())
            {
                var t = string.Join(",", positions.Select(i => i.ToString()));
                foreach (var item in positions)
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        result = item.ToString();
                    }
                    else
                        result += "," + item.ToString();
                }
            }
            else
                result = "0";
            return result;
        }
    }
}
