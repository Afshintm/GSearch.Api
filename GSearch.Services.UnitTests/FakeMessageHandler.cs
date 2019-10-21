using System.IO;
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
                using (var streamReader = new StreamReader(@".\StubSearch.html"))
                {
                    var strHtml = streamReader.ReadToEnd();
                    var response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(strHtml) };
                    return response;
                }
            });
        }
    }
}
