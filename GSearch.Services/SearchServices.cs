using System;
using System.Collections.Generic;

namespace GSearch.Services
{
    public interface ISearchServices {
        IEnumerable<string> Search(string url, IEnumerable<string> keywords, int pages = 20);
    }
    public class SearchServices : ISearchServices
    {
        public IEnumerable<string> Search(string url, IEnumerable<string> keywords, int pages = 20)
        {
            if (keywords == null) yield break;
            foreach (var item in keywords)
            {
                yield return item;
            }
        }
    }
}
