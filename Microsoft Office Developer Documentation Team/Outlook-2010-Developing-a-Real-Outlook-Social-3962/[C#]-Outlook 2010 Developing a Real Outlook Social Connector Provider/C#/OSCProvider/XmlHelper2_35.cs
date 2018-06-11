using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSCProvider
{
    internal abstract partial class XmlHelper
    {
        public static string HtmlEncode(string txt)
        {
            return System.Web.HttpUtility.HtmlEncode(txt);
        }
    }
}
