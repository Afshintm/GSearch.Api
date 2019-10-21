using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GSearch.Services.UnitTests
{
    public class GoogleSearchTests
    {
        FakeMessageHandler _fakeMessageHandler;
        FakeMessageHandler FakeMessageHandler
        {
            get { 
                if(_fakeMessageHandler==null)
                    _fakeMessageHandler = new FakeMessageHandler();
                return _fakeMessageHandler;
            } }

        HttpClient _stub_HttpClient;
        HttpClient Stub_HttpClient
        {
            get
            {
                if (_stub_HttpClient == null)
                {
                    _stub_HttpClient = new HttpClient(FakeMessageHandler);
                    _stub_HttpClient.BaseAddress = new Uri("https://www.google.com.au/search");
                }
                return _stub_HttpClient;
            }
        }
        //GoogleSearch _googleSearch;
        GoogleSearch GoogleSearch =>  new GoogleSearch(Stub_HttpClient);

        [Fact]
        public void TestGoogleSearchReturnsTheHtml()
        {
            var result = Task.Run(async () => await GoogleSearch.SearchAsync("online title search")).Result;
            Assert.NotNull(result);
        }
        [Fact]
        public async Task TestSearchServiceFindsItemsInSpecificHtmlFile()
        {
            var mockLogger = new Mock<ILogger<SearchServices>>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())[It.IsAny<string>()]).Returns(@"<div class=""ZINbbc xpd O9g5cc uUPGi"">");

            var searchServices = new SearchServices(GoogleSearch, mockLogger.Object, mockConfiguration.Object);
            var result = await searchServices.SearchAsync("www.infotrack.com.au", "online title search");
            Assert.NotNull(result);
            Assert.DoesNotContain("0", result);
            
        }
    }
}
