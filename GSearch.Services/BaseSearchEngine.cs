using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GSearch.Services
{
    public interface IBaseSearchEngine { 
        HttpClient HttpClient { get; set; }
        Task<string> Search(string keywords, int numberOfResult = 0);
        Task<string> SearchAsync(string keywords, int numberOfResult = 0);
        string SetupBaseUrlWithParams(string keywords, int numberOfResult = 0);
    }
    public abstract class BaseSearchEngine: IBaseSearchEngine
    {
        public HttpClient HttpClient { get; set; }
        public BaseSearchEngine(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public virtual async Task<string> Search(string keywords, int numberOfResult = 0)
        {
            if (string.IsNullOrEmpty(keywords))
                throw new ApplicationException("Search kewords cannot be null");

            var response = await HttpClient.GetAsync(HttpClient.BaseAddress + SetupBaseUrlWithParams(keywords, numberOfResult));
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        public virtual async Task<string> SearchAsync(string keywords, int numberOfResult = 0)
        {
            if (string.IsNullOrEmpty(keywords))
                throw new ApplicationException("Search kewords cannot be null");

            var request = new HttpRequestMessage(HttpMethod.Get, HttpClient.BaseAddress + SetupBaseUrlWithParams(keywords, numberOfResult));
            var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync();
        }
        public virtual string SetupBaseUrlWithParams(string keywords, int numberOfResult = 0)
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

