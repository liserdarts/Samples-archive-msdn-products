using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalSiteBrandingEditWeb.Pages
{
    public partial class SetHostTheme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Uri hostWeb = new Uri(Request.QueryString["SPHostUrl"]);

            // Notice that we use direct client context, not using SP App auth pattern. This is so that we execute the code
            // only in the context of the particular user and ignoring app permissions completely. 
            using (ClientContext clientContext = new ClientContext(hostWeb))
            {
                Microsoft.SharePoint.Client.Web rootWeb = clientContext.Web;
                clientContext.Load(rootWeb);
                clientContext.ExecuteQuery();

                rootWeb.ApplyTheme(URLCombine(rootWeb.ServerRelativeUrl, "/_catalogs/theme/15/palette008.spcolor"),
                                    URLCombine(rootWeb.ServerRelativeUrl, "/_catalogs/theme/15/fontscheme003.spfont"),
                                    null, false);
                clientContext.ExecuteQuery();
            }
        }

        private string URLCombine(string baseUrl, string relativeUrl)
        {
            if (baseUrl.Length == 0)
                return relativeUrl;
            if (relativeUrl.Length == 0)
                return baseUrl;
            return string.Format("{0}/{1}", baseUrl.TrimEnd(new char[] { '/', '\\' }), relativeUrl.TrimStart(new char[] { '/', '\\' }));
        }
    }
}