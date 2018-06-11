using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalSiteBrandingEditWeb.Pages
{
    public partial class ModifyPersonalMySite : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Uri hostWeb = new Uri(Request.QueryString["SPHostUrl"]);

            // Notice that we use direct client context, not using SP App auth pattern. This is so that we execute the code
            // only in the context of the particular user and ignoring app permissions completely. 
            using (ClientContext clientContext = new ClientContext(hostWeb))
            {
                // Get user profile
                ProfileLoader loader = Microsoft.SharePoint.Client.UserProfiles.ProfileLoader.GetProfileLoader(clientContext);
                UserProfile profile = loader.GetUserProfile(); ;
                Microsoft.SharePoint.Client.Site personalSite = profile.PersonalSite;

                clientContext.Load(personalSite);
                clientContext.ExecuteQuery();

                // Let's check if the site already exists
                if (personalSite.ServerObjectIsNull.Value)
                {
                    // Let's queue the personal site creation using oob timer job based approach
                    // Using async mode, since end user could go away from browser, you could do this using oob web part as well
                    profile.CreatePersonalSiteEnque(true);
                    clientContext.ExecuteQuery();
                    Response.Write("No my site exists. Currently provisioning...");
                }
                else
                {
                    // Site already exists, let's modify the branding by applyign a theme... just as well you could upload
                    // master page and set that to be shown. Notice that you can also modify this code to change the branding
                    // later and updates would be reflected whenever user visits my site host... or any other location where this
                    // app part is located. You could place this also to front page of the intranet for ensuring that it's applied.
                    using (ClientContext subContext = new ClientContext(personalSite.Url))
                    {
                        // Let's update the theme colors of the my site
                        Microsoft.SharePoint.Client.Web rootWeb = subContext.Web;
                        subContext.Load(rootWeb);
                        subContext.ExecuteQuery();

                        rootWeb.ApplyTheme(URLCombine(rootWeb.ServerRelativeUrl, "/_catalogs/theme/15/palette008.spcolor"),
                                            URLCombine(rootWeb.ServerRelativeUrl, "/_catalogs/theme/15/fontscheme003.spfont"),
                                            null, false);
                        subContext.ExecuteQuery();

                        // Just to output status
                        Response.Write("My site exists: " + personalSite.Url + " - web title - " + subContext.Web.Title);
                    }

                }
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