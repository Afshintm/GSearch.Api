using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GSearch.Services
{
    public interface ISearchSource { 
    }
    public class SearchSource:ISearchSource
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SearchSource(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }
        
    }
}
