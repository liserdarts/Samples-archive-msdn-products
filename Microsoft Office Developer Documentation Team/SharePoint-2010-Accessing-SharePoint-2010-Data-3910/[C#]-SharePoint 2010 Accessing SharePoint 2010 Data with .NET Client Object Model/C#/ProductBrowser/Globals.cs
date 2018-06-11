using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;

namespace ProductBrowser
{
    public static class Globals
    {
        public static ClientContext ClientContext;
        public static ListItemCollection ProductCategories { get; set; }
    }
}
