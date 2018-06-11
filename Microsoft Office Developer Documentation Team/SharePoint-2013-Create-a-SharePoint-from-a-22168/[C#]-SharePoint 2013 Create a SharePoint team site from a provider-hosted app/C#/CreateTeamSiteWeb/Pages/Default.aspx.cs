using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateTeamSiteWeb.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected string AccessToken
        {
            get
            {
                if (ViewState["accessToken"] == null)
                {
                    throw new NullReferenceException("Access token is empty.");
                }
                else
                {
                    return (string)ViewState["accessToken"];
                }
            }
            set
            {
                ViewState["accessToken"] = value;
            }
        }
        protected string HostWeb
        {
            get
            {
                if (ViewState["hostWeb"] == null)
                {
                    throw new NullReferenceException("HostWeb is empty.");
                }
                else
                {
                    return (string)ViewState["hostWeb"];
                }
            }
            set 
            {
                ViewState["hostWeb"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // The following code gets the client context and Title property by using TokenHelper.
                // To access other properties, you may need to request permissions on the host web.

                var contextToken = TokenHelper.GetContextTokenFromRequest(Page.Request);
                HostWeb = Page.Request["SPHostUrl"];

                SharePointContextToken spContextToken = TokenHelper.ReadAndValidateContextToken(contextToken, Request.Url.Authority);
                AccessToken = TokenHelper.GetAccessToken(spContextToken, new Uri(HostWeb).Authority).AccessToken;
            }
        }

        protected void btnCreateSite_Click(object sender, EventArgs e)
        {
            using (var ctx = TokenHelper.GetClientContextWithAccessToken(HostWeb, AccessToken))
            {
                try
                {
                    ctx.Load(ctx.Web);
                    ctx.Load(ctx.Web.Navigation.QuickLaunch);
                    ctx.ExecuteQuery();

                    string newSiteUrl = txtSiteTitle.Text.Replace(" ","");
                    var web = CreateSubSite(ctx, txtSiteTitle.Text, newSiteUrl ,"STS#0");
                    AddQuickLaunch(ctx.Web.Navigation.QuickLaunch, txtSiteTitle.Text, ctx.Web.Url + "/" + newSiteUrl);
                    ctx.ExecuteQuery();
                    lnkNewSite.Text = txtSiteTitle.Text;
                    lnkNewSite.NavigateUrl = ctx.Web.Url + "/" + newSiteUrl;
                }
                catch (Exception ex)
                {
                    lnkNewSite.Text = string.Empty;
                    lbMessage.Text = ex.Message;
                }
            }
        }

        protected static Web CreateSubSite(ClientContext ctx, string Title, string Url, string Template)
        {
            WebCreationInformation wci_projects = new WebCreationInformation();
            wci_projects.Title = Title;
            wci_projects.Url = Url;
            wci_projects.UseSamePermissionsAsParentSite = true;
            wci_projects.WebTemplate = Template;
            wci_projects.Language = 1033; //LCID
            Web web_project = ctx.Web.Webs.Add(wci_projects);
            return web_project;
        }

        public static void AddQuickLaunch(NavigationNodeCollection quickLaunch, string Title, string Url, bool isChildNode = false)
        {
            NavigationNodeCreationInformation navci = new NavigationNodeCreationInformation();
            navci.AsLastNode = true;
            navci.Title = Title;
            navci.Url = Url;
            if (!isChildNode)
                quickLaunch.Add(navci);
            else
                quickLaunch[quickLaunch.Count - 1].Children.Add(navci);
        }
    }
}