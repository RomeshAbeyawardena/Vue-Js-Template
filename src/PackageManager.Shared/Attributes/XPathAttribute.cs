using System;

namespace PackageManager.Shared.Attributes
{
    public class XPathAttribute : Attribute
    {
        public XPathAttribute(string xPath)
        {
            XPath = xPath;
        }

        public string XPath { get; }
    }
}
