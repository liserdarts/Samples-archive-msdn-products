using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSCProvider
{
    internal abstract partial class XmlHelper
    {
        internal static string HtmlEncode(string input)
        {
            return System.Net.WebUtility.HtmlEncode(input);
        }
    }
}
