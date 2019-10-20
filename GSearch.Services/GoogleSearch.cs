using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace GSearch.Services
{
    public class GoogleSearch
    {
        private readonly HttpClient _httpClient;
        public GoogleSearch(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public string SearchQuery { get; set; }

        string GetSearchQuery() {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                var splitted = SearchQuery.Split(new char[] { ' ', ',', '+' });
                result = "?q=" + string.Join("+", splitted);
            }
            return result;
        }
        public async Task<string> Search()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + GetSearchQuery());
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
