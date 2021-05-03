using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
