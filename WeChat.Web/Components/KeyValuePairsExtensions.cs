using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XData.Data.Extensions
{
    public static class KeyValuePairsExtensions
    {
        public static string GetValue(this IEnumerable<KeyValuePair<string, string>> nameValuePairs, string key)
        {
            string value = null;
            if (nameValuePairs.Any(p => p.Key == key))
            {
                value = nameValuePairs.First(p => p.Key == key).Value;
            }
            return value;
        }
    }
}
