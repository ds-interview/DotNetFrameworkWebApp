using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Core
{
    public static class Extensions
    {
        public static void AddOrReplace(this IDictionary<string, object> dictionaryContainer, string key, object value)
        {
            if (dictionaryContainer.ContainsKey(key))
            {
                dictionaryContainer[key] = value;
            }
            else
            {
                dictionaryContainer.Add(key, value);
            }
        }

        public static string ToPascalCase(this string s)
        {
            var words = s.Split(new[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(word => word.Substring(0, 1).ToUpper() +
                                         word.Substring(1).ToLower());

            var result = String.Concat(words)?.Trim();
            return result;
        }
    }
}
