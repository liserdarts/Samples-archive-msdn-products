using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SharePoint.Client;
using System.Threading;

namespace MSDN.SharePoint.Samples.ProductBrowser
{
    public static class Global
    {
        private static ClientContext _clientContext = null;
        public static ListItemCollection ProductCategories {get; set;}

        public static ClientContext GetClientContext()
        {
            if (_clientContext == null)
                _clientContext = new ClientContext("http://intranet.wingtip.com");

            return _clientContext;
        }
    }
}
