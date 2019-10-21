using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSearch.Services
{
    public interface IGenericSearchServices<T>: ISearchServices
    {
        T SearchEngin { get; set; }
        ///string DivSearchResultRegexPattern { get; set; }

    }
    public class GenericSearchServices<T> : IGenericSearchServices<T> where T: IBaseSearchEngine 
    {
        public T SearchEngin { get; set; }
        public IConfiguration Configuration { get; set; }
        public ILogger<GenericSearchServices<T>> Logger { get; set; }
        public string DivSearchResultRegexPattern { get; set; }

        public GenericSearchServices(T searchEngin, ILogger<GenericSearchServices<T>> logger, IConfiguration configuration)
        {
            SearchEngin = searchEngin;
            Configuration = configuration;
            Logger = logger;
            var TName = typeof(T).Name;
            DivSearchResultRegexPattern = Configuration.GetSection("SearchResultHtmlDivRegex")[TName]; 
        }

        public string Search(string url, string keywords, int numberofResult = 0)
        {
            var result = string.Empty;
            try
            {
                var searchresult = SearchEngin.Search(keywords, numberofResult).Result;

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
                Logger.LogError(ex.Message);
                throw;
            }

            return result;
        }
        public async Task<string> SearchAsync(string url, string keywords, int numberofResult = 0)
        {
            var result = string.Empty;
            try
            {
                var searchresult = await SearchEngin.SearchAsync(keywords, numberofResult);

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
                Logger.LogError(ex.Message);
                throw;
            }
            return result;
        }

        
    }
}
