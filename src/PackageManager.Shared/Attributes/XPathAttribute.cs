using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
