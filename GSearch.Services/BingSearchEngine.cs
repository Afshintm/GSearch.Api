using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GSearch.Services
{
    public interface IBingSearchEngine : IBaseSearchEngine { }
    public class BingSearchEngine:BaseSearchEngine, IBingSearchEngine
    {
        public BingSearchEngine(HttpClient httpclient) : base(httpclient) { }
    }
}
