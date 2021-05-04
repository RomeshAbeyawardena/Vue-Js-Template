using System.Collections.Generic;
using System.Linq;

namespace PackageManager.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool TryParseBool(this string value)
        {
            return (bool.TryParse(value, out var result) && result);
        }

        public static string Format(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        public static string Format(this string value, IDictionary<string, string> replacementKeyValues)
        {
            foreach (var (key, val) in replacementKeyValues)
            {
                value = value.Replace(key, val);
            }

            return value;
        }

        public static string Concat(this string value, params string[] values)
        {
            return new string(Enumerable
                .Concat(value.ToCharArray(), values
                    .SelectMany(c => c.ToCharArray()))
                .ToArray());
        }

        public static string ToCamelCase(this string value)
        {
            return new string(value.Select((c, i) => i == 0 ? char.ToLower(c) : c)
                .ToArray());
        }
    }
}
