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
    }
}
