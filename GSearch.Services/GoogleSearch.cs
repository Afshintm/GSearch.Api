using System.Net.Http;
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
        public int NumberOfResult { get; set; }
        string GetSearchQuery() {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                var splitted = SearchQuery.Split(new char[] { ' ', ',', '+' });
                result = "?q=" + string.Join("+", splitted);
                if (NumberOfResult > 0) result += $"&num={NumberOfResult}";
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
        public async Task<string> Search_v1()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,_httpClient.BaseAddress + GetSearchQuery());
            var response = await _httpClient.SendAsync(request,HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
