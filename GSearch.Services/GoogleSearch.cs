using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace GSearch.Services
{
    public class GoogleSearch
    {
        private readonly HttpClient _httpClient;
        public GoogleSearch(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Search(string keywords, int numberOfResult = 0)
        {
            if (string.IsNullOrEmpty(keywords))
                throw new ApplicationException("Search kewords cannot be null");

            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + SetupBaseUrlWithParams(keywords, numberOfResult));
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        public async Task<string> SearchAsync(string keywords, int numberOfResult = 0)
        {
            if (string.IsNullOrEmpty(keywords))
                throw new ApplicationException("Search kewords cannot be null");

            var request = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + SetupBaseUrlWithParams(keywords, numberOfResult));
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync();
        }

        private string SetupBaseUrlWithParams(string keywords, int numberOfResult = 0)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(keywords))
            {
                var splitted = keywords.Split(new char[] { ' ', ',', '+' });
                result = "?q=" + string.Join("+", splitted);
                if (numberOfResult > 0) result += $"&num={numberOfResult}";
            }
            return result;
        }
    }
}