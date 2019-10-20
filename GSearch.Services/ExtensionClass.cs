using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
namespace GSearch.Services
{
    public static class ExtensionClass
    {
        /// <summary>
        /// finds the position of a key within an string that is separated by Separator
        /// </summary>
        /// <param name="input"></param>
        /// <param name="Separator"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IEnumerable<int> SplitIndexOf(this string input,string Separator, string key) {
            if (string.IsNullOrEmpty(input))
                yield break;

                int order = -1;
                var divs = Regex.Split(input, Separator);
                if (divs == null) yield break;
                
                foreach (var item in divs)
                {
                    order++;
                    if (item.Contains(key))
                    {
                        yield return order;
                    }
                }
                if (order==-1)
                yield return -1;

        }
    }
}
