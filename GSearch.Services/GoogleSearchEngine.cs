using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GSearch.Services
{
    
    public interface IGoogleSearchEngine : IBaseSearchEngine { }
    public class GoogleSearchEngine : BaseSearchEngine, IGoogleSearchEngine
    {
        public GoogleSearchEngine(HttpClient httpclient) : base(httpclient) { }
    }

}
