using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GSearch.Services.UnitTests
{
    class FakeMessageHandler:HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent("a test content") };
                return response;
            });
        }
    }
}
