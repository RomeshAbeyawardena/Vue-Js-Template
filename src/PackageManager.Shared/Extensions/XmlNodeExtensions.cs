using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace PackageManager.Shared.Extensions
{
    public static class XmlNodeExtensions
    {
        public static IDictionary<string, object> ToDictionary(this XmlNode xmlNode,
            params string[] values)
        {
            var valueDictionary = new Dictionary<string, object>();
            foreach (var value in values)
            {
                var attributes = xmlNode.Attributes;
                valueDictionary.Add(value,
                    attributes[value]?.Value
                    ?? attributes[value.ToCamelCase()]?.Value
                    ?? attributes[value.ToLower()]?.Value
                    ?? attributes[value.ToUpper()]?.Value);
            }

            return valueDictionary;
        }

        public static T GetValues<T>(this XmlNode node,
            IServiceProvider serviceProvider = default,
            params object[] args)
        {
            var type = typeof(T);
            var result = type.Resolve<T>(serviceProvider, args);
            result.Update(node
                .ToDictionary(type.GetProperties().Select(a => a.Name).ToArray()));

            return result;
        }
    }
}
